namespace FanFiction.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int StoryId { get; set; }

        public string CommentAuthor { get; set; }

        public DateTime CommentedOn { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }
    }
}