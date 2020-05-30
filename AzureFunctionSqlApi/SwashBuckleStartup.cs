using System.Reflection;
using AzureFunctionSqlApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs;

[assembly: WebJobsStartup(typeof(SwashBuckleStartup))]

namespace AzureFunctionSqlApi
{
  internal class SwashBuckleStartup : IWebJobsStartup
  {
    public void Configure(IWebJobsBuilder builder)
    {
      builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
    }
  }
}