using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        IMongoCollection<Product> productCollection { get; }
        IEnumerable<Product> GetAllProducts();
        Product GetProductDetails(string _id);
        void Create(Product product);
        void Update(string _id, Product prodcut);
        void Delete(string _id); 
    }
}
