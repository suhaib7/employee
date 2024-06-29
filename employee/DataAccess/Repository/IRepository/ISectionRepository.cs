using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ISectionRepository : IRepository<Section>
    {
        public void Update(Section section);
        public Task<List<Department>> Departments();
    }
}
