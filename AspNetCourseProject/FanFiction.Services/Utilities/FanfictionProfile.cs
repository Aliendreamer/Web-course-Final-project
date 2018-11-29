namespace FanFiction.Services.Utilities
{
    using AutoMapper;
    using Models;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;

    public class FanfictionProfile : Profile
    {
        public FanfictionProfile()
        {
            CreateMap<RegisterInputModel, FanFictionUser>();

            CreateMap<FanFictionStory, StoryOutputModel>();

            CreateMap<Announcement, AnnouncementOutputModel>();
        }
    }
}