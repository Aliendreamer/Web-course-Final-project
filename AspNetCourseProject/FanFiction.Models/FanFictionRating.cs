namespace FanFiction.Models
{
    public class FanFictionRating
    {
        public int RatingId { get; set; }
        public StoryRating StoryRating { get; set; }

        public int FanFictionId { get; set; }
        public FanFictionStory FanFictionStory { get; set; }
    }
}