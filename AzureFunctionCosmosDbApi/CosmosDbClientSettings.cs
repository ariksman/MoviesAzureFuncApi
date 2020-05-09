namespace AzureFunctionCosmosDbApi
{
  public class CosmosDbClientSettings
  {
    public string DatabaseName { get; set; }
    public string ContainerName { get; set; }
    public string EndPointUrl { get; set; }
    public string AuthorizationKey { get; set; }
  }
}