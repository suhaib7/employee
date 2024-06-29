using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTraining>()
            .HasKey(et => new { et.EmployeeId, et.TrainingId });

            modelBuilder.Entity<EmployeeTraining>()
                .HasOne(et => et.Employee)
                .WithMany(e => e.EmployeeTrainings)
                .HasForeignKey(et => et.EmployeeId);

            modelBuilder.Entity<EmployeeTraining>()
                .HasOne(et => et.Training)
                .WithMany(t => t.EmployeeTrainings)
                .HasForeignKey(et => et.TrainingId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = "12345678", Phone = "12345678", RoleId = 1 }
                );

            modelBuilder.Entity<Department>().HasData(
                    new Department { Id = 1, NameEN = "Development", NameAR = "تطوير" },
                    new Department { Id = 2, NameEN = "Business", NameAR = "عمل" },
                    new Department { Id = 3, NameEN = "HR", NameAR = "الموارد البشرية" }
            );
            modelBuilder.Entity<Section>().HasData(
                    new Section { Id = 2, NameEN = "Mobile", NameAR = "موبايل", DepartmentId = 1},
                    new Section { Id = 3, NameEN = "Analysis", NameAR = "تحليل", DepartmentId = 2},
                    new Section { Id = 4, NameEN = "HR Manager", NameAR = "مدير موارد", DepartmentId = 3}
            );

            modelBuilder.Entity<Training>().HasData(
                    new Training { Id = 1, Name = "Net Core" },
                    new Training { Id = 2, Name = "Flutter" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Employee" }
            );
        }
    }
}
