using Admin.Core;
using System.Text.Json.Serialization;

namespace Admin.Api.Resources
{
    public class GroupResource
    {
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ExpiredOrCancelled { get; set; }
        public DateTime ActualEndDate { get; set; }
        public ServiceResource Service { get; set; }
        public int NumberOfLessonsPerWeek { get; set; }
        public int DaysOfTheWeekWithlessons { get; set; }
        public LessonTypeResource LessonType { get; set; }
        public DateTime OnlinePassDate { get; set; }
        public TutorResource Tutor { get; set; }
        public BranchResource Branchs { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan StartTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan EndTime { get; set; }
        public int CountOfStuudents { get; set; }
        public int NewStudentsCount { get; set; }
        public int FreezingStudentNumber { get; set; }
        public int NumberOfStudentsDroppingOut { get; set; }
        public bool IsDeleted { get; set; } = false;
        //   public int FinalStudentCount { get; set; }
    }
}
