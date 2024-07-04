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
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly IMongoDatabase _db;

        public EmployeeRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Employee> employeeCollection => 
            _db.GetCollection<Employee>("employees");

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeCollection.Find(u => true).ToList();
        }

        public Employee GetEmployeeDetails(string id)
        {
            var employee = employeeCollection.Find(u => u._id == id).FirstOrDefault();
            return employee;
        }

        public void Create(Employee employee)
        {
            employeeCollection.InsertOne(employee);
        }
        public void Update(string id, Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", id);
            var update = Builders<Employee>.Update
                .Set("Name", employee.Name)
                .Set("Email", employee.Email)
                .Set("RoleId", employee.RoleId)
                .Set("Password", employee.Password);

            employeeCollection.UpdateOne(filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", ObjectId.Parse(id));
            employeeCollection.DeleteOne(filter);
        }
    }
}
