using Admin.Core;
using System.Text.Json.Serialization;

namespace Admin.Api.Resources
{
    public class SaveGroupResource
    {

        public int GroupNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ExpiredOrCancelled { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int ServiceId { get; set; }
        public int NumberOfLessonsPerWeek { get; set; }
        public int DaysOfTheWeekWithlessons { get; set; }
        public int LessonTypeId { get; set; }
        public DateTime OnlinePassDate { get; set; }
        public int TutorId { get; set; }
        public int BranchId { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan StartTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan EndTime { get; set; }
        public int CountOfStuudents { get; set; }
        public int NewStudentsCount { get; set; }
        public int FreezingStudentNumber { get; set; }
        public int NumberOfStudentsDroppingOut { get; set; }
        //  public int FinalStudentCount { get; set; }
    }
}
