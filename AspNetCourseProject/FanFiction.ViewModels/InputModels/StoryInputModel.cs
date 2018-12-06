namespace FanFiction.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class StoryInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string Summary { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        [Display(Name = "Story Image")]
        [DataType(DataType.Upload)]
        public IFormFile StoryImage { get; set; }
    }
}