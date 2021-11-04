using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration _config;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public CatalogContext(IConfiguration config)
        {
            _config = config;
            _client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            _database = _client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = _database.GetCollection<Product>(_config.GetValue<string>("DatabaseSettings:CollectionName"));

            SeedDatabase();
        }

        public IMongoCollection<Product> Products { get; }

        public void SeedDatabase()
        {
            CatalogContextSeed.SeedData(Products);
        }
    }
}
