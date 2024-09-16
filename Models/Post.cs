namespace Blog_Management_System.Models
{
    public class Post
    {
        [Required]
        public int PostID { get; set; }
        [Required]
        [MaxLength(100)]
        public String? Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public String? Content { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public int UserID { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        
        public List<Comment>? Comments { get; set; }
        
        public List<Tag>? Tags { get; set; }
        
        public List<Like>? Likes { get; set; }


    }
}
