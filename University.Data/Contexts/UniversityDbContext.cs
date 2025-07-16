using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data.Contexts.ClassMappings;
using University.Core.Entities;

namespace University.Data.Contexts
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; } // ✅ Added for Course


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StudentMapping());
            modelBuilder.ApplyConfiguration(new CourseMapping()); // ✅ Added

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=UniversityDB;Trusted_Connection=True;TrustServerCertificate=True;");

        }


    }
}
