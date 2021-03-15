using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using static System.Console;

namespace azure_resources_mapper
{
    class Program
    {
        private const string Client = "";
        private const string Secret = "";
        private const string Tenant = "";

        static void Main(string[] args)
        {
            WriteLine("Mapper Started");

            var azure = AuthenticateAzure(Client, Secret, Tenant);
            WriteLine("Authenticated");

            foreach (var resourceGroup in azure.ResourceGroups.List())
            {
                WriteLine($"GROUP {resourceGroup.Name}");
                foreach (var servicePlan in azure.AppServices.AppServicePlans.ListByResourceGroup(resourceGroup.Name))
                {
                    WriteLine($" --> {servicePlan.Name}");
                    foreach (var webApp in azure.AppServices.WebApps.ListWebAppBasic())
                    {
                        WriteLine($"  --> {string.Join(",", webApp.HostNames)}");
                        foreach (var VARIABLE in azure.WebApps.GetById(webApp.Id).)
                        {
                            
                        }
                    }
                }
            }

        }

        static IAzure AuthenticateAzure(string client, string secret, string tenant)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                client, 
                secret, 
                tenant,
                AzureEnvironment.AzureGlobalCloud
            );

            return Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();
        }
    }
}
