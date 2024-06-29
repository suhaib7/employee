using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IDepartmentRepository Department { get; }
        ISectionRepository Section { get; }
        ITrainingRepository Training { get; }
        IEmployeeTrainingRepository EmployeeTraining { get; }

        void Save();
    }
}
