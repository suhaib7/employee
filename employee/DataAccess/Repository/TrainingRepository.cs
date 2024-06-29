using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        private ApplicationDbContext _db;
        public TrainingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Training> GetAllTrainings()
        {
            var traingins = _db.Trainings.ToList();
            return traingins;
        }

        public void Update(Training training)
        {
            _db.Trainings.Update(training);
        }
    }
}
