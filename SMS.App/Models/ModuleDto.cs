namespace SMS.App.Models
{
    public class ModuleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? LessonCount { get; set; }
        public TimeSpan? Duration { get; set; }

        public List<LessonDto>? Lessons { get; set; }
        public List<CourseDto>? UsedInCourses { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ModuleCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<int> LessonIds { get; set; }
        public List<int> UsedInCourseIds { get; set; }

    }

    public class ModuleUpdateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<int> LessonIds { get; set; }
        public List<int> UsedInCourseIds { get; set; }
    }
}
