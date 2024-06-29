using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ITrainingRepository : IRepository<Training>
    {
        public void Update(Training training);
        public List<Training> GetAllTrainings();
    }
}
