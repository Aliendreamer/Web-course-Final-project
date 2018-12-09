namespace FanFiction.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChapterInputModel
    {
        public string Author { get; set; }

        [Required]
        public int StoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(3000, MinimumLength = 500)]
        public string Content { get; set; }
    }
}