namespace FanFiction.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChapterInputModel
    {
        public string Author { get; set; }

        [Required]
        public int StoryId { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(3000, ErrorMessage = "Your chapter should not have more than 3000 characters")]
        public string Content { get; set; }
    }
}