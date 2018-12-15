namespace FanFiction.ViewModels.InputModels
{
    using System;
    using Utilities;
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int StoryId { get; set; }

        public string CommentAuthor { get; set; }

        public DateTime CommentedOn { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.CommentLength)]
        public string Message { get; set; }
    }
}