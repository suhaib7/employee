using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOkWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IEmployeeRepository Employee {  get; private set; }

        public IDepartmentRepository Department { get; private set; }

        public ISectionRepository Section { get; private set; }

        public ITrainingRepository Training { get; private set; }

        public IEmployeeTrainingRepository EmployeeTraining { get; private set; }

        public UnitOkWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            Department = new DepartmentRepository(_db);
            Section = new SectionRepository(_db);
            Training = new TrainingRepository(_db);
            EmployeeTraining = new EmployeeTrainingRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
