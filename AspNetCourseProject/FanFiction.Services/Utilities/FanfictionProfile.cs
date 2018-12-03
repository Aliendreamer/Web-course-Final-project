namespace FanFiction.Services.Utilities
{
    using System;
    using Models;
    using System.Linq;
    using AutoMapper;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Users;
    using ViewModels.OutputModels.Stories;
    using ViewModels.OutputModels.Announcements;

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

            CreateMap<FanFictionUser, UserOutputViewModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Username, cfg => cfg.MapFrom(x => x.UserName))
                .ForMember(x => x.NickName, cfg => cfg.MapFrom(x => x.Nickname))
                .ForMember(x => x.Email, cfg => cfg.MapFrom(x => x.Email))
                .ForMember(x => x.Role, cfg => cfg.Ignore())
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Count))
                .ForMember(x => x.MessagesCount, opt => opt.MapFrom(x => x.SendMessages.Count + x.SendMessages.Count))
                .ForMember(x => x.Stories, opt => opt.Ignore())
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.SendMessages.Concat(x.ReceivedMessages)))
                .ForMember(x => x.Notifications, o => o.MapFrom(x => x.Notifications))
                .ForMember(x => x.UserStories, o => o.Ignore())
                .ForMember(x => x.FollowedStories, o => o.Ignore())
                .ForMember(x => x.Friends, opt => opt.MapFrom(x => x.Friends.Count))
                .ForMember(x => x.BlockedUsers, o => o.MapFrom(x => x.BlockedUsers.Count));

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

            CreateMap<FanFictionStory, StoryOutputModel>()
                .ForMember(opt => opt.Ratings, cfg => cfg.MapFrom(x => x.Ratings.Select(z => z.StoryRating.Rating)))
                .ForMember(opt => opt.Followers, cfg => cfg.MapFrom(x => x.Followers.Select(xx => xx.FanFictionUser)))
                .ForMember(x => x.LastEditedOn, opt => opt.MapFrom(x => x.LastEditedOn.Date))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date))
                .ForMember(o => o.Rating, opt => opt.Ignore())
                .ForMember(x => x.Summary, opt => opt.NullSubstitute(GlobalConstants.NoSummary))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))
                .ForMember(x => x.Chapters, opt => opt.MapFrom(x => x.Chapters))
                .ForMember(x => x.Title, o => o.MapFrom(x => x.Title))
                .ForMember(x => x.Type, o => o.MapFrom(x => x.Type))
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.ImageUrl, o => o.AllowNull());

            CreateMap<Chapter, ChapterOutputModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Length, o => o.MapFrom(x => x.Content.Length))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.FanFictionUser.Nickname));

            CreateMap<Comment, CommentOtputModel>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.FanFictionUser.UserName))
                .ForMember(x => x.CommentedOn, o => o.MapFrom(x => x.CommentedOn.Date))
                .ForMember(x => x.Message, o => o.MapFrom(x => x.Message));

            CreateMap<StoryType, StoryTypeOutputModel>()
                .ForMember(x => x.Type, opt => opt.MapFrom(z => z.Name)).ReverseMap();
        }
    }
}