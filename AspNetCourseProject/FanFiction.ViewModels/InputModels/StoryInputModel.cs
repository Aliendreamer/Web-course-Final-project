namespace FanFiction.ViewModels.InputModels
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class StoryInputModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public IFormFile StoryImage { get; set; }
    }
}