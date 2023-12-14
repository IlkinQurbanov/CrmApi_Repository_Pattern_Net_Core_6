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
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .HasKey(g => g.Id);

            builder
                .Property(g => g.Id)
                .UseIdentityColumn();

            builder
                .Property(g => g.GroupNumber)
                .IsRequired();

            builder
                .Property(g => g.StartDate)
                .IsRequired();

            builder
                .Property(g => g.EndDate)
                .IsRequired();

            builder
                .Property(g => g.ActualEndDate)
                .IsRequired();

            builder
                .Property(g => g.ServiceId)
                .IsRequired();

            builder
                .Property(g => g.NumberOfLessonsPerWeek)
                .IsRequired();

            builder
                .Property(g => g.DaysOfTheWeekWithlessons)
                .IsRequired();

            builder
                .Property(g => g.LessonTypeId)
                .IsRequired();

            builder
                .Property(g => g.OnlinePassDate)
                .IsRequired();

            builder
                .Property(g => g.TutorId)
                .IsRequired();

            builder
                .Property(g => g.StartTime)
                .IsRequired();

            builder
                .Property(g => g.EndTime)
                .IsRequired();

            builder
                .Property(g => g.CountOfStuudents)
                .IsRequired();

            builder
                .Property(g => g.NewStudentsCount)
                .IsRequired();

            builder
                .Property(g => g.FreezingStudentNumber)
                .IsRequired();

            builder
                .Property(g => g.NumberOfStudentsDroppingOut)
                .IsRequired();


            builder
                .HasOne(g => g.Service)
                .WithMany()
                .HasForeignKey(g => g.ServiceId);

            builder
                .HasOne(g => g.LessonType)
                .WithMany()
                .HasForeignKey(g => g.LessonTypeId);

            builder
                .HasOne(g => g.Tutor)
                .WithMany()
                .HasForeignKey(g => g.TutorId);
            builder
             .HasOne(g => g.Branch)
             .WithMany()
             .HasForeignKey(g => g.BranchId);

            builder
                .ToTable("Groups");
        }

    }

}

