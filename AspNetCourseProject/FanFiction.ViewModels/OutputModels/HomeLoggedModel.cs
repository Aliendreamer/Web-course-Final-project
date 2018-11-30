namespace FanFiction.ViewModels.OutputModels
{
    using System.Collections.Generic;
    using Stories;

    public class HomeLoggedModel
    {
        public ICollection<StoryHomeOutputModel> Stories { get; set; }

        public ICollection<AnnouncementOutputModel> Announcements { get; set; }
    }
}