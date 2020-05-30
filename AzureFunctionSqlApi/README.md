
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
