using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;
using University.Core.Entities.Identity;

namespace University.Data.Contexts.ClassMappings
{
    public class UserLoginMapping : IEntityTypeConfiguration<UserLogin>

    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");
            builder.HasKey(s => s.UserId);
        }
    }
}
