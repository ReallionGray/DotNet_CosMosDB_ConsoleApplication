using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace Console_Api.Models.Data
{
    public class CosMosDBContext
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        // The Cosmos client instance
        private CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "Assessment";
        //private string containerId = "items";


        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }
        
        private async Task CreateContainerAsync(string containerId)
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, $"/{containerId}", 500);
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }
        public async Task<Container> CreateContextAsync(string containerId)
        {
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync(containerId);
            return this.container;
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }


        //private static readonly Database _database = cosmosClient.CreateDatabaseIfNotExistsAsync("Assessment");
        //public Container Program => _database.GetContainer("Program");

    }
}
