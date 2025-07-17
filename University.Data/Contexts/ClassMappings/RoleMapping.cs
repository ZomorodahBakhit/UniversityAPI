using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;
using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>

    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("RoleID");

        }
    }
}
