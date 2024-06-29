using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public IEnumerable<EmployeeDTO> GetAllEmployeesWithTrainings()
        {
            var employees = _db.Employees
                .Include(e => e.EmployeeTrainings)
                .ThenInclude(et => et.Training)
                .ToList();

            var employeeDTOs = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Trainings = e.EmployeeTrainings.Select(et => new TrainingDTO
                {
                    Id = et.Training.Id,
                    Name = et.Training.Name
                }).ToList()
            });

            return employeeDTOs;
        }


        public void Update(Employee employee)
        {
            _db.Employees.Update(employee);
        }
    }
}
