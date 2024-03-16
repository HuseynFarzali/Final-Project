using System;

namespace SMS.App.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string MessageContent { get; set; }

        public int ChatId { get; set; }
        public ChatDto? Chat { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class MessageCreateDto 
    {
        public int AppUserId { get; set; }
        public int ChatId { get; set; }
        public string? MessageContent { get; set; }
    }
}
