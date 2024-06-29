using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmployeeTrainingRepository : Repository<EmployeeTraining>, IEmployeeTrainingRepository
    {
        private ApplicationDbContext _db;
        public EmployeeTrainingRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public List<Employee> GetEnrolledEmployeesById(int trainingId)
        {
            return _db.EmployeeTrainings
                .Where(et => et.TrainingId == trainingId)
                .Select(et => et.Employee)
                .ToList();
        }

        public void Update(EmployeeTraining employeeTraining)
        {
            _db.EmployeeTrainings.Update(employeeTraining);
        }
    }
}
