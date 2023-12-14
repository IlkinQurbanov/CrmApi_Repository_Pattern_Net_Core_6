namespace Admin.Api.Resources
{
    public class LessonTypeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
