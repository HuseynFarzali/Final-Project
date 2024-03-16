namespace SMS.App.Models
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname => $"{Name} {Surname}";

        public string? Description { get; set; }

        public List<int>? InstructedCourseIds { get; set; }
        public List<CourseDto>? InstructedCourses { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? ImageId { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
