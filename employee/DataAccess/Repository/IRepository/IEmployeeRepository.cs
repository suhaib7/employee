using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public void Update(Employee employee);
        IEnumerable<EmployeeDTO> GetAllEmployeesWithTrainings();
    }
}
