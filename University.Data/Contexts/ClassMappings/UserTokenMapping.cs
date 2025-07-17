using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;
using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class UserTokenMapping : IEntityTypeConfiguration<UserToken>

    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(s => s.UserId);
        }
    }
}
