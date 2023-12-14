using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Core.Models;

namespace Admin.Dsta.Configurations
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder
                 .HasKey(a => a.Id);
            builder
                .Property(m => m.Id)
                .UseIdentityColumn();
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(90);
            builder
                .ToTable("Tutors");
        }
    }
}
