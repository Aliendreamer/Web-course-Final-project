namespace FanFiction.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

        public int StoryId { get; set; }
        public FanFictionStory FanFictionStory { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Message { get; set; }

        [Required]
        public DateTime CommentedOn { get; set; }
    }
}