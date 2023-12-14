using Admin.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dsta.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
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
                .ToTable("Departments");
        }
    }
}
