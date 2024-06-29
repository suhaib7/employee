using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Initialize()
        {
            try {
                if (_db.Database.GetPendingMigrations().Count() > 0) { 
                    _db.Database.Migrate();
                }
            }catch (Exception) { }

            if (!_db.Employees.Any(e => e.RoleId == 1)) {
                var adminEmployee = new Employee { 
                    Name = "admin",
                    Email = "admin@gmail.com",
                    RoleId = 1,
                    Phone = "000000000"
                };
                _db.Employees.Add(adminEmployee);
                _db.SaveChanges();
            }
            
        }
    }
}
