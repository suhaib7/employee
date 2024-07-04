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
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly IMongoDatabase _db;

        public DepartmentRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Department> departmentCollection => 
            _db.GetCollection<Department>("departments");

        public IEnumerable<Department> GetAllDepartments()
        {
            return departmentCollection.Find(u => true).ToList();
        }

        public Department GetDepartmentDetails(string id)
        {
            var department = departmentCollection.Find(u => u._id == id).FirstOrDefault();
            return department;
        }

        public void Create(Department department)
        {
            departmentCollection.InsertOne(department);
        }
        public void Update(string id, Department department)
        {
            var filter = Builders<Department>.Filter.Eq("_id", id);
            var update = Builders<Department>.Update
                .Set("NameEN", department.NameEN)
                .Set("NameAR", department.NameAR);

            departmentCollection.UpdateOne(filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<Department>.Filter.Eq("_id", ObjectId.Parse(id));
            departmentCollection.DeleteOne(filter);
        }
    }
}
