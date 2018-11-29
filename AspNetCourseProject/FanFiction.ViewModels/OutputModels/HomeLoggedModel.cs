namespace FanFiction.ViewModels.OutputModels
{
    using System.Collections.Generic;

    public class HomeLoggedModel
    {
        public ICollection<StoryOutputModel> Stories { get; set; }

        public ICollection<AnnouncementOutputModel> Announcements { get; set; }
    }
}