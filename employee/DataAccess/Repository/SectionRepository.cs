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
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        private ApplicationDbContext _db;
        public SectionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Department>> Departments()
        {
            var departments = new List<Department>();

            var allDepartments = await _db.Departments.ToListAsync();
            if (allDepartments?.Any() == true) {
                foreach (var department in allDepartments) {
                    departments.Add(new Department() { 
                        NameEN = department.NameEN,
                        NameAR = department.NameAR,
                        Id = department.Id,
                    });
                }
            }
            return departments;
        }

        public void Update(Section section)
        {
            _db.Sections.Update(section);
        }
    }
}
