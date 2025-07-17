using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;
using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class UserClaimMapping : IEntityTypeConfiguration<UserClaim>

    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("UserClaimID");

        }
    }
}
