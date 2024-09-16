namespace Blog_Management_System.Models
{
    public class Tag
    {
        [Required]
        public int TagID { get; set; }
        [Required]
        [MaxLength(25)]
        public String? TagName { get; set; }
        public int PostID { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }

    }
}
