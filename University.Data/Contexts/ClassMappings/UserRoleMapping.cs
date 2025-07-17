using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;
using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>

    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(s => new { s.UserId, s.RoleId });
        }
    }
}
