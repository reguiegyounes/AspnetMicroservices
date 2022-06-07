using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly DatabaseSettings _settings;

        public CatalogContext(IConfiguration configuration,IOptions<DatabaseSettings> settings)
        {
            this._settings = settings.Value;
            var client=new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            Products = database.GetCollection<Product>(_settings.CollectionName);
            CatalogContextSeed.SeedData(Products);
            
        }
        public IMongoCollection<Product> Products { get; }
    }
}
