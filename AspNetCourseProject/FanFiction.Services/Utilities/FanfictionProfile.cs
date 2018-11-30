namespace FanFiction.Services.Utilities
{
    using System.Linq;
    using AutoMapper;
    using Models;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;
    using ViewModels.OutputModels.Stories;

    public class FanfictionProfile : Profile
    {
        public FanfictionProfile()
        {
            CreateMap<RegisterInputModel, FanFictionUser>();

            CreateMap<FanFictionStory, StoryOutputModel>();

            CreateMap<FanFictionStory, StoryHomeOutputModel>()
                .ForMember(opt => opt.Author, cfg => cfg.MapFrom(x => x.Author.Nickname))
                .ForMember(opt => opt.Title, cfg => cfg.MapFrom(x => x.Title))
                .ForMember(opt => opt.StoryType, cfg => cfg.MapFrom(x => x.Type.Name))
                .ForMember(opt => opt.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(opt => opt.CreatedOn, cfg => cfg.MapFrom(x => x.CreatedOn.ToShortDateString()))
                .ForMember(opt => opt.Summary, cfg => cfg.MapFrom(x => x.Summary != null ? x.Summary.Substring(0, 50) : GlobalConstants.NoSummary))
                .ForMember(opt => opt.Rating, cfg => cfg.Condition(x => x.Ratings.Any()))
                .ForMember(opt => opt.Rating, cfg => cfg.MapFrom(x => x.Ratings.Any() ? x.Ratings.Average(r => r.StoryRating.Rating) : 0));

            CreateMap<Announcement, AnnouncementOutputModel>()
                .ForMember(opt => opt.Author, cfg => cfg.MapFrom(x => x.Author.UserName))
                .ForMember(opt => opt.Content, cfg => cfg.MapFrom(x => x.Content))
                .ForMember(opt => opt.PublishedOn, cfg => cfg.MapFrom(x => x.PublshedOn.ToShortDateString()))
                .ForMember(opt => opt.Id, cfg => cfg.MapFrom(x => x.Id));
        }
    }
}