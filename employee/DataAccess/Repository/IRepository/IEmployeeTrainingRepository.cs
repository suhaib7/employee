using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IEmployeeTrainingRepository : IRepository<EmployeeTraining>
    {
        public void Update(EmployeeTraining employeeTraining);
        public List<Employee> GetEnrolledEmployeesById(int trainingId);
    }
}
