namespace SMS.App.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int>? EnrolledCourseIds { get; set; }
        public List<CourseDto>? EnrolledCourses { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? ImageId { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class StudentCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class StudentUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int>? EnrolledCourseIds { get; set; }
        public int? AppUserId { get; set; }
    }
}
