using Admin.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dsta.Configurations
{
    public class LessonTypeConfiguration : IEntityTypeConfiguration<LessonType>
    {
        public void Configure(EntityTypeBuilder<LessonType> builder)
        {
            builder
                 .HasKey(a => a.Id);
            builder
                .Property(m => m.Id)
                .UseIdentityColumn();
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .ToTable("LessonTypes");
        }

    }
}
