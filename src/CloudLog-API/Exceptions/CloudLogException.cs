namespace CloudLogAPI.Exceptions;

public class CloudLogException : Exception
{
    public CloudLogException(string message) : base(message) {}

    public CloudLogException(string message, Exception innerException) : base(message, innerException) {}
}