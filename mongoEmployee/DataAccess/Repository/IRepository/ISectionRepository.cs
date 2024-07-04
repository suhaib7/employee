using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ISectionRepository
    {
        IMongoCollection<Section> sectionCollection { get; }
        IEnumerable<Section> GetAllsections();
        Section GetSectionDetails(string _id);
        void Create(Section section);
        void Update(string _id, Section section);
        void Delete(string _id);
    }
}
