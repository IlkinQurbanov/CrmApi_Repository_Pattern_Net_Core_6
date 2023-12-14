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

    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                 .HasKey(a => a.Id);
            builder
                .Property(m => m.Id)
                .UseIdentityColumn();
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .ToTable("Services");
        }
    }
}
