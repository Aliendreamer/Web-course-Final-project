namespace FanFiction.ViewModels.OutputModels.Chapters
{
    using System;
    using Utilities;
    using System.ComponentModel.DataAnnotations;

    public class ChapterEditModel
    {
        public int Id { get; set; }

        public int StoryId { get; set; }

        [StringLength(ViewModelsConstants.TitleMaxLength, MinimumLength = ViewModelsConstants.TitleMinLength)]
        public string Title { get; set; }

        public int Length => this.Content?.Length ?? 0;

        public string Author { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.ChapterLength, MinimumLength = ViewModelsConstants.ChapterMinLength, ErrorMessage = ViewModelsConstants.ChapterInputContentError)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}