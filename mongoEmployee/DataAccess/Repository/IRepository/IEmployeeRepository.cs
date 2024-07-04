using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        IMongoCollection<Employee> employeeCollection { get; }
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeDetails(string _id);
        void Create(Employee employee);
        void Update(string _id, Employee employee);
        void Delete(string _id); 
    }
}
