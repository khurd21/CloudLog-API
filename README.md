# Cloud Log API

<img
    src="https://img.shields.io/badge/dotnet-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"
    alt="Website Badge" />
<img
    src="https://img.shields.io/badge/CSharp-239120?style=for-the-badge&logo=csharp&logoColor=white"
    alt="Website Badge" />

## LOGO TO GO HERE

![Cloud Log API Build](https://github.com/khurd21/CloudLog-API/actions/workflows/cloud-log-api-build.yml/badge.svg)

Cloud Log API is the backend component to a Logbook Application for skydivers to log their jumps digitally.

---


### How to Setup Locally

You will need the following dependencies:

- Docker
- Docker Compose
- Dotnet 8
- AWS CLI for DynamoDB

Using the [script](./resources/dynamodb/cloudlog-api-tables.sh), run the following command with docker running.

```sh
docker-compose build
docker-compose up
./resources/dynamodb/cloudlog-api-tables.sh --local
```

To list the tables: `aws dynamodb list-tables --endpoint-url http://localhost:8000`

To delete the tables: 
```sh
aws dynamodb delete-table --table-name LoggedJump --endpoint-url http://localhost:8000
aws dynamodb delete-table --table-name UserInfo --endpoint-url http://localhost:8000
```