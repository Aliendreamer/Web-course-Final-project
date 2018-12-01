namespace FanFiction.ViewModels.OutputModels.Announcements
{
    using System.Collections.Generic;
    using InputModels;

    public class AllAnnouncementsModel
    {
        public AllAnnouncementsModel()
        {
            this.Announcements = new List<AnnouncementOutputModel>();
        }

        public AnnouncementInputModel Announcement { get; set; }

        public ICollection<AnnouncementOutputModel> Announcements { get; set; }
    }
}