namespace FanFiction.Models
{
    using System.Collections.Generic;

    public class StoryRating
    {
        public int Id { get; set; }

        public StoryRating()
        {
            this.FanFictionRatings = new HashSet<FanFictionRating>();
        }

        public double Rating { get; set; }

        public string UserId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }

        public ICollection<FanFictionRating> FanFictionRatings { get; set; }
    }
}