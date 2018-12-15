namespace FanFiction.ViewModels.InputModels
{
    using System;
    using Utilities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class StoryInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.TitleMaxLength, MinimumLength = ViewModelsConstants.TitleMinLength)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ViewModelsConstants.StorySummaryLength)]
        public string Summary { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        [Display(Name = ViewModelsConstants.StoryImageDisplay)]
        [DataType(DataType.Upload)]
        public IFormFile StoryImage { get; set; }
    }
}