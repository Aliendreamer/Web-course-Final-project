namespace FanFiction.Models
{
    public class UserStory
    {
        public string FanfictionUserId { get; set; }
        public FanFictionUser FanFictionUser { get; set; }

        public int StoryId { get; set; }
        public FanFictionStory FanFictionStory { get; set; }
    }
}