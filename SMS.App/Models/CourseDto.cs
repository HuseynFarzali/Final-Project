namespace SMS.App.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public List<int>? EnrolledStudentIds { get; set; }
        public List<StudentDto>? EnrolledStudents { get; set; }
        public CategoryDto? Category { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
