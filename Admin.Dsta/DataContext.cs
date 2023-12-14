using Admin.Core.Models;
using Admin.Dsta.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Dsta
{

    public class DataContext : DbContext
    {
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Department> Departments { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
      
            builder
             .ApplyConfiguration(new TutorConfiguration());
            builder
            .ApplyConfiguration(new LessonTypeConfiguration());
            builder
            .ApplyConfiguration(new ServiceConfiguration());
            builder
          .ApplyConfiguration(new BranchConfiguration());
            builder
        .ApplyConfiguration(new DepartmentConfiguration());




        }
    }
}
