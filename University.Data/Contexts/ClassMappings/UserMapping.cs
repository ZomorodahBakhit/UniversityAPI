using Microsoft.EntityFrameworkCore;
using University.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace University.Data.Contexts.ClassMappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            // Configure the User entity properties
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("UserID");
                
        }
    }
};