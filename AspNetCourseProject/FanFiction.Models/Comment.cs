namespace FanFiction.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public FanFictionUser FanFictionUser { get; set; }

        public int? StoryId { get; set; }

        [ForeignKey("StoryId")]
        public virtual FanFictionStory FanFictionStory { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; }

        [Required]
        public DateTime CommentedOn { get; set; }
    }
}