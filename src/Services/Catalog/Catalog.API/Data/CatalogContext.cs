using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var settings = configuration.GetValue<DatabaseSettings>("DatabaseSettings");
            var client=new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Products = database.GetCollection<Product>(settings.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
