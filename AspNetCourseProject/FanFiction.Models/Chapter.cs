namespace FanFiction.Models
{
    using System;

    public class Chapter
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Length => this.Content.Length;

        public string AuthorId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }

        public int FanFictionStoryId { get; set; }
        public FanFictionStory FanFictionStory { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}