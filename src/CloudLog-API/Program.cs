using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Asp.Versioning;
using CloudLogAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

////////////////////////////////////////
/// Dependency Injections
////////////////////////////////////////

/// <summary>
/// Creates the AmazonDynamoDBConfig injection.
/// </summary>
builder.Services.AddSingleton<AmazonDynamoDBConfig>(context =>
{
    var configuration = context.GetRequiredService<IConfiguration>();
    IConfigurationSection section = configuration.GetSection("DynamoDB");
    if (section.GetValue("LocalMode", false))
    {
        return new()
        {
            ServiceURL = section.GetValue<string>("ServiceUrl"),
        };
    }
    return new()
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(section.GetValue<string>("Region")),
    }; 
});

/// <summary>
/// Creates the AmazonDynamoDBClient injection.
/// </summary>
builder.Services.AddSingleton<AmazonDynamoDBClient>(context =>
{
    var config = context.GetRequiredService<AmazonDynamoDBConfig>(); 
    return new(config);
});

/// <summary>
/// Creates the DynamoDBContextConfig injection.
/// </summary>
builder.Services.AddSingleton<DynamoDBContextConfig>(context =>
{
    var configuration = context.GetRequiredService<IConfiguration>();
    IConfigurationSection section = configuration.GetSection("DynamoDb");
    return new()
    {
        ConsistentRead = section.GetValue<bool>("ConsistentRead"),
        IgnoreNullValues = section.GetValue<bool>("IgnoreNullValues"),
        SkipVersionCheck = section.GetValue<bool>("SkipVersionCheck"),
        TableNamePrefix = section.GetValue<string>("TableNamePrefix"),
    };
});

/// <summary>
/// Creates the DynamoDBContext injection.
/// </summary>
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>(context =>
{
    var dynamoDBContextConfig = context.GetRequiredService<DynamoDBContextConfig>();
    var amazonDynamoDBClient = context.GetRequiredService<AmazonDynamoDBClient>();
    return new(amazonDynamoDBClient, dynamoDBContextConfig);
});

/// <summary>
/// Creates the LogbookService injection.
/// </summary>
/// <typeparam name="ILogbookService">Interface for which to inject.</typeparam>
/// <typeparam name="LogbookService">Implementing class of ILogbookService</typeparam>
/// <returns>The transient that implements the ILogbookService.</returns>
builder.Services.AddTransient<ILogbookService, LogbookService>(context =>
{
    var logger = context.GetRequiredService<ILogger<ILogbookService>>();
    var dynamoDBContext = context.GetRequiredService<IDynamoDBContext>();
    return new(logger, dynamoDBContext);
});

/// <summary>
/// Creates the UserInfoService injection.
/// </summary>
/// <typeparam name="IUserInfoService">Interface for which to inject.</typeparam>
/// <typeparam name="UserInfoService">Implementing class of IUserInfoService.</typeparam>
/// <value></value>
builder.Services.AddTransient<IUserInfoService, UserInfoService>(context =>
{
    var dynamoDBContext = context.GetRequiredService<IDynamoDBContext>();
    return new(dynamoDBContext);
});

///////////////////////////////////////////////
/// 3rd Party Additions
///////////////////////////////////////////////

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.UseGoogle(
            clientId: builder.Configuration["Authentication:Google:ClientId"]!
        );
    });

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// If the environment is development, we want to automatically create the tables that will automatically exist in DynamoDB.
// TODO: Is this the best way to do it? What if we created a script that made it for us?
if (app.Environment.IsDevelopment())
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
