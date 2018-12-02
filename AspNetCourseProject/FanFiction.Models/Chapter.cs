namespace FanFiction.Models
{
    using System;
    using System.Collections.Generic;

    public class Chapter
    {
        public Chapter()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public double Length => this.Content.Length;

        public string AuthorId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }

        public int FanFictionStoryId { get; set; }
        public FanFictionStory FanFictionStory { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}