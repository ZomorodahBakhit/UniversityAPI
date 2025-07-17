using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Entities.Identity;
using University.Data.Contexts.ClassMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace University.Data.Contexts
{
    public class UniversityDbContext : IdentityDbContext
        <User,
        Role,
        int,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>
    {

        public UniversityDbContext(DbContextOptions options)
            : base(options)
        {
        }   
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; } // ✅ Added for Course


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StudentMapping());
            modelBuilder.ApplyConfiguration(new CourseMapping()); // ✅ Added


            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new UserClaimMapping());
            modelBuilder.ApplyConfiguration(new UserRoleMapping());
            modelBuilder.ApplyConfiguration(new UserLoginMapping());
            modelBuilder.ApplyConfiguration(new RoleClaimMapping());
            modelBuilder.ApplyConfiguration(new UserTokenMapping());

        }




    }
}
