using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        IMongoCollection<Department> departmentCollection { get; }
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentDetails(string _id);
        void Create(Department department);
        void Update(string _id, Department department);
        void Delete(string _id); 
    }
}
