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
    public class SectionRepository : ISectionRepository
    {
        public readonly IMongoDatabase _db;
        public SectionRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Section> sectionCollection => 
            _db.GetCollection<Section>("sections");

        public IEnumerable<Section> GetAllsections()
        {
            return sectionCollection.Find(u => true).ToList();
        }

        public Section GetSectionDetails(string id)
        {
            var section = sectionCollection.Find(u => u._id == id).FirstOrDefault();
            return section;
        }

        public void Create(Section section)
        {
            sectionCollection.InsertOne(section);
        }

        public void Update(string id, Section section)
        {
            var filter = Builders<Section>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<Section>.Update
                .Set("NameEN", section.NameEN)
                .Set("NameAR", section.NameAR)
                .Set("DepartmentId", section.DepartmentId);

            sectionCollection.UpdateOne(filter, update);
        }

        public void Delete(string id)
        {
            var filter = Builders<Section>.Filter.Eq("_id", ObjectId.Parse(id));
            sectionCollection.DeleteOne(filter);
        }
    }
}
