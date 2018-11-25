namespace FanFiction.Models
{
    using System;

    public class Announcement
    {
        public int Id { get; set; }

        public DateTime PublshedOn { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
        public FanFictionUser Author { get; set; }
    }
}