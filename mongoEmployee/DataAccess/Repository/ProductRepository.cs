using DataAccess.Repository.IRepository;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly IMongoDatabase _db;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "productCacheKey";
        public ProductRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Product> productCollection => 
            _db.GetCollection<Product>("products");

        public void Create(Product product)
        {
            productCollection.InsertOne(product);
        }

        public void Delete(string _id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(_id));
            productCollection.DeleteOne(filter);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productCollection.Find(u => true).ToList();
        }

        public Product GetProductDetails(string _id)
        {
            var product = productCollection.Find(u => u._id == _id).FirstOrDefault();
            return product;
        }

        public void Update(string _id, Product prodcut)
        {
            var filter = Builders<Product>.Filter.Eq("_id", _id);
            var update = Builders<Product>.Update
                .Set("Name", prodcut.Name);

            productCollection.UpdateOne(filter, update);
        }
    }
}
