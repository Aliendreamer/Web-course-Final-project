namespace FanFiction.Services.Utilities
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Models;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;
    using ViewModels.OutputModels.Announcements;
    using ViewModels.OutputModels.Stories;
    using ViewModels.OutputModels.Users;

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

            CreateMap<FanFictionUser, UserAdminViewModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.UserName))
                .ForMember(x => x.NickName, cfg => cfg.MapFrom(x => x.Nickname))
                .ForMember(x => x.Email, cfg => cfg.MapFrom(x => x.Email))
                .ForMember(x => x.Role, cfg => cfg.Ignore())
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Count))
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.SendMessages.Count + x.SendMessages.Count))
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.FanFictionStories.Count));

            CreateMap<FanFictionUser, ChangingRoleModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Nickname, cfg => cfg.MapFrom(x => x.Nickname))
                .ForMember(x => x.Role, cfg => cfg.Ignore())
                .ForMember(x => x.NewRole, cfg => cfg.Ignore())
                .ForMember(x => x.AppRoles, cfg => cfg.Ignore());

            CreateMap<AnnouncementInputModel, Announcement>()
                .ForMember(opt => opt.Content, cfg => cfg.MapFrom(x => x.Content))
                .ForMember(opt => opt.Author, cfg => cfg.Ignore())
                .ForMember(x => x.PublshedOn, opt => opt.MapFrom(o => DateTime.UtcNow));
        }
    }
}