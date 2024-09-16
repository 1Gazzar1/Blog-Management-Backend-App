

namespace Blog_Management_System.Models
{
    public enum CommentStatus
    {
        Pending,Rejected,Approved
    }
    public class Comment
    {
        [Required]
        public int CommentID { get; set; }
        [Required]
        [MaxLength(500)]
        public String? Content { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public CommentStatus Status { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        
        public int UserID { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        
        public int PostID { get; set; }

    }

}
