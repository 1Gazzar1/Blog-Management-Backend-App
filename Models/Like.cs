namespace Blog_Management_System.Models
{
    public class Like
    {
        [Required]
        public int LikeID { get; set; }


        public int UserID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public int PostID { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }

    }
}
