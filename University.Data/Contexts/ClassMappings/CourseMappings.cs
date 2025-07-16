using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Core.Entities;

namespace University.Data.Contexts.ClassMappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);  // Primary Key

            builder.Property(c => c.Id)
                .HasColumnName("CourseID");  // Custom column name (optional)

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(256);

            

            builder.Property(c => c.CreatedTime)
                .IsRequired();

            builder.Property(c => c.LastUpdatedTime)
                .IsRequired();
        }
    }
}
