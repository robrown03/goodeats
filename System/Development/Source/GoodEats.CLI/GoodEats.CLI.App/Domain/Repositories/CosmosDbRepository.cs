using GoodEats.CLI.Configurations;
using GoodEats.CLI.Core;
using GoodEats.CLI.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Spatial;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace GoodEats.CLI.Domain.Repositories
{
    public interface ICosmosDbRepository : IClientContextRepository
    {
        /// <summary>
        /// Upserts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<T> UpsertItem<T>(T item) where T : CosmosEntityBase;
       
    }
    public class CosmosDbRepository : ICosmosDbRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly CosmosConfig _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CosmosDbRepository(IOptions<CosmosConfig> configuration)
        {
            _configuration = configuration.Value;
            var clientOptions = new CosmosClientOptions
            {
                MaxRetryAttemptsOnRateLimitedRequests = 3,
                MaxRetryWaitTimeOnRateLimitedRequests = new TimeSpan(0, 0, 30),
                ConnectionMode = ConnectionMode.Gateway
            };
            _cosmosClient = new CosmosClient(_configuration.EndpointUri, _configuration.AccountKey, clientOptions);
        }


        /// <summary>
        /// Upserts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public async Task<T> UpsertItem<T>(T item) where T : CosmosEntityBase
        {
            var container = await GetContainerReference();
            var reponse = await container.UpsertItemAsync(item, partitionKey: new PartitionKey(item.DocType));

            return reponse.Resource;
        }

        private async Task<Container> GetContainerReference()
        {
            var database = (await _cosmosClient.CreateDatabaseIfNotExistsAsync(_configuration.DbName)).Database;
            ContainerProperties containerProperties = new ContainerProperties()
            {
                Id = _configuration.DbContainerName,
                PartitionKeyPath = "/docType",
                IndexingPolicy = new IndexingPolicy()
                {
                    Automatic = true,
                    IndexingMode = IndexingMode.Consistent
                },

            };
            var spaitalPath = new SpatialPath()
            {
                Path = "/location/*"
            };
            spaitalPath.SpatialTypes.Add(SpatialType.Point);
            containerProperties.IndexingPolicy.SpatialIndexes.Add(spaitalPath);


            var containerResponse = await database.CreateContainerIfNotExistsAsync(containerProperties);

            if (containerResponse.StatusCode == HttpStatusCode.OK || containerResponse.StatusCode == HttpStatusCode.Created)
            {
                return containerResponse.Container;
            }
            else
            {
                throw new Exception($"{containerResponse.StatusCode}-{containerResponse.StatusCode.ToString()}");
            }
        }
    }
}
