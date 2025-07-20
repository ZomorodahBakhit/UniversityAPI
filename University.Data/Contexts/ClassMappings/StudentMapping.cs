using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using University.Core.Entities;

namespace University.Data.Contexts.ClassMappings
{
    public class StudentMapping : IEntityTypeConfiguration<Student>

    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("StudentID");
            builder.HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId);

            builder.Property(s => s.Name).HasMaxLength(256);
        }
    }
}
