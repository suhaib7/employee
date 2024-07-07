using DataAccess.Repository.IRepository;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        public readonly IMongoDatabase _db;
        public TrainingRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Training> trainingCollection =>
            _db.GetCollection<Training>("training");

        public IEnumerable<Training> GetAllTrainings()
        {
            return trainingCollection.Find(u => true).ToList();
        }

        public Training GetTrainingDetails(string _id)
        {
            var training = trainingCollection.Find(u => u._id == _id).FirstOrDefault();
            return training;
        }

        public void Create(Training training)
        {
            trainingCollection.InsertOne(training);
        }

        public void Update(string _id, Training training)
        {
            var filter = Builders<Training>.Filter.Eq("_id", _id);
            var update = Builders<Training>.Update
                .Set("Name", training.Name)
                .Set("Hours", training.Hours);

            trainingCollection.UpdateOne(filter, update);
        }

        public void Delete(string _id)
        {
            var filter = Builders<Training>.Filter.Eq("_id", ObjectId.Parse(_id));
            trainingCollection.DeleteOne(filter);
        }

        public IEnumerable<Training> GetTrainingsByIds(List<string> trainingIds)
        {
            var filter = Builders<Training>.Filter.In(t => t._id, trainingIds);
            return trainingCollection.Find(filter).ToList();
        }
    }
}
