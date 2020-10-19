# Introduction

Demo app using Azure functions as an API to fetch movies from third party source.

![System components](/Documents/MoviesAzureFuncStructure.png)

## Getting Started

Install first the [azure functions core tools](https://github.com/Azure/azure-functions-core-tools) via ```npm```

``` PowerShell
npm i -g azure-functions-core-tools@3 --unsafe-perm true
```

## Local Azure functions development

Use the command line to start the functions locally by

``` PowerShell
func start
```

To test locally with the Azure Storage emulator, you will need the following `local.settings.json` file to be set up:

```js
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureWebJobs.FinnkinoMovieQueryFunction.Disabled": false,
    "AzureWebJobs.GetMovies.Disabled": false
  },
  "ConnectionStrings": {
    "AzureSqlConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinnkinoMovies;Integrated Security=True;"
  },
  "AzureSqlSettings": {
    "ConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinnkinoMovies;Integrated Security=True;"
  }
}
```

Additionally add this to project debug settings as input parameters to ensure CORS is in effect:
```
host start --build --port 7071 --cors * --pause-on-error
```
