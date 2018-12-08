namespace FanFiction.ViewModels.OutputModels.Stories
{
    using System;

    public class ChapterInputModel
    {
        public string Author { get; set; }

        public int Length { get; set; }

        public int StoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }
    }
}