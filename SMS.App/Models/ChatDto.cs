namespace SMS.App.Models
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<MessageDto>? Messages { get; set; }
        public List<AppUser>? AppUsers { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ChatCreateDto
    {
        public string? Name { get; set; }
        public List<int> AppUserIds { get; set; }
    }

    public class ChatUpdateDto
    {
        public string? Name { get; set; }
        public List<int> AppUserIds { get; set; }
    }
}
