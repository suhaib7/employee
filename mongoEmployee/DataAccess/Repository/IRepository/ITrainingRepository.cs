using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ITrainingRepository
    {
        IMongoCollection<Training> trainingCollection { get; }
        IEnumerable<Training> GetAllTrainings();
        IEnumerable<Training> GetTrainingsByIds(List<string> trainingIds);
        Training GetTrainingDetails (string _id);
        void Create(Training training);
        void Update(string _id,Training training);
        void Delete(string _id);

    }
}
