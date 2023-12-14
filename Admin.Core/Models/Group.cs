using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Admin.Core.Models
{

    public class Group
    {
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ExpiredOrCancelled { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int NumberOfLessonsPerWeek { get; set; }
        public int DaysOfTheWeekWithlessons { get; set; }
        public int LessonTypeId { get; set; }
        public LessonType LessonType { get; set; }
        public DateTime OnlinePassDate { get; set; }
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan StartTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan EndTime { get; set; }
        public int CountOfStuudents { get; set; }
        public int NewStudentsCount { get; set; }
        public int FreezingStudentNumber { get; set; }
        public int NumberOfStudentsDroppingOut { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        // public int FinalStudentCount { get; set; }
    }
}
