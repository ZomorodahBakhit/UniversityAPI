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

            builder.Property(s => s.Name).HasMaxLength(256);
        }
    }
}
