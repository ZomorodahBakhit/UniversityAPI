using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>

    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("RoleClaimID");

        }
    }
}
