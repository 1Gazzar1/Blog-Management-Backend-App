namespace Blog_Management_System.Models
{
    public enum Role
    {
        Admin,
        Editor,
        Reader
    }

    public class User
    {
        public int UserID { get; set; }
        [Required]
        [MaxLength(30)]
        public String? Name { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        [MaxLength(50)]
        public String? Email { get; set; }
        [Required]
        [MinLength(8)]
        public String? Password { get; set; }

        [JsonIgnore]
        public List<Like>? Likes { get; set; }
        [JsonIgnore]
        public List<Comment>? Comments { get; set; }
        public List<Post>? Posts { get; set; }

    }
}
