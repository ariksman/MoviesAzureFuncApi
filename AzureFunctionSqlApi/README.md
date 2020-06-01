
## Getting Started

To test locally with the Azure Storage emulator, you will need the following `local.settings.json` file to be set up:

```js
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },
  "Host": {
    "CORS": "*"
  },
  "ConnectionStrings": {
    "AzureSqlConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinnkinoMovies;Integrated Security=True;"
  },
  "AzureSqlSettings": {
    "ConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinnkinoMovies;Integrated Security=True;"
  } 
}
```

## Database

``` Powershell
dotnet ef database update -p [project with migrations] -s [api project]
```

## Swagger

[Swagger](https://swagger.io) is an OpenAPI description format for REST APIs and allows you to describe the structure of your APIs so that machines can read them.
The automatically generated UI from available endpoint functions can be found at the http://localhost:7071/api/swagger/ui
