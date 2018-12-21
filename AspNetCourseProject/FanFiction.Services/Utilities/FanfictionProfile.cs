namespace FanFiction.Services.Utilities
{
    using System;
    using Models;
    using AutoMapper;
    using System.Linq;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Users;
    using ViewModels.OutputModels.Stories;
    using ViewModels.OutputModels.InfoHub;
    using ViewModels.OutputModels.Announcements;
    using ViewModels.OutputModels.Chapters;

    public class FanfictionProfile : Profile
    {
        public FanfictionProfile()
        {
            CreateMap<RegisterInputModel, FanFictionUser>();

            CreateMap<FanFictionStory, StoryOutputModel>()
                .ForMember(opt => opt.Ratings, cfg => cfg.MapFrom(x => x.Ratings.Select(z => z.StoryRating.Rating)))
                .ForMember(opt => opt.Followers, cfg => cfg.MapFrom(x => x.Followers.Select(xx => xx.FanFictionUser)))
                .ForMember(x => x.LastEditedOn, opt => opt.MapFrom(x => x.LastEditedOn.Date))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date))
                .ForMember(o => o.Rating, opt => opt.Ignore())
                .ForMember(o => o.Author, opt => opt.MapFrom(x => x.Author))
                .ForMember(x => x.Summary, opt => opt.NullSubstitute(GlobalConstants.NoSummary))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))
                .ForMember(x => x.Chapters, opt => opt.MapFrom(x => x.Chapters))
                .ForMember(x => x.Title, o => o.MapFrom(x => x.Title))
                .ForMember(x => x.Type, o => o.MapFrom(x => x.Type))
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.ImageUrl, o => o.AllowNull());

            CreateMap<FanFictionStory, StoryHomeOutputModel>()
                .ForMember(opt => opt.Author, cfg => cfg.MapFrom(x => x.Author.UserName))
                .ForMember(opt => opt.Title, cfg => cfg.MapFrom(x => x.Title))
                .ForMember(opt => opt.StoryType, cfg => cfg.MapFrom(x => x.Type.Name))
                .ForMember(opt => opt.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(opt => opt.CreatedOn, cfg => cfg.MapFrom(x => x.CreatedOn.ToShortDateString()))
                .ForMember(opt => opt.Summary, cfg => cfg.MapFrom(x => x.Summary ?? GlobalConstants.NoSummary))
                .ForMember(opt => opt.Rating, cfg => cfg.MapFrom(x => x.Ratings.Any() ? x.Ratings.Average(r => r.StoryRating.Rating) : GlobalConstants.Zero));

            CreateMap<StoryInputModel, FanFictionStory>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.LastEditedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.Summary, o => o.MapFrom(x => x.Summary))
                .ForMember(x => x.Title, o => o.MapFrom(x => x.Title))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<FanFictionStory, StoryDetailsOutputModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.ToShortDateString()))
                .ForMember(x => x.LastEditedOn, opt => opt.MapFrom(x => x.LastEditedOn.ToShortDateString()))
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.StoryType, opt => opt.MapFrom(x => x.Type.Name))
                .ForMember(x => x.Rating, cfg => cfg.MapFrom(x => x.Rating))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(x => x.ImageUrl))
                .ForMember(opt => opt.Summary, cfg => cfg.NullSubstitute(GlobalConstants.NoSummary));

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
                .ForMember(x => x.BlockedUsers, x => x.Ignore())
                .ForMember(x => x.BlockedBy, o => o.Ignore());

            CreateMap<FanFictionUser, BlockedUserOutputModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Nickname, opt => opt.MapFrom(x => x.Nickname));

            CreateMap<FanFictionUser, ChangingRoleModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Nickname, cfg => cfg.MapFrom(x => x.Nickname))
                .ForMember(x => x.Role, cfg => cfg.Ignore())
                .ForMember(x => x.NewRole, cfg => cfg.Ignore())
                .ForMember(x => x.AppRoles, cfg => cfg.Ignore());

            CreateMap<FanFictionUser, UserOutputStoryModel>()
                .ForMember(x => x.Username, o => o.MapFrom(z => z.UserName))
                .ForMember(x => x.Nickname, o => o.MapFrom(z => z.Nickname))
                .ForMember(x => x.Id, o => o.MapFrom(z => z.Id));

            CreateMap<AnnouncementInputModel, Announcement>()
                .ForMember(opt => opt.Content, cfg => cfg.MapFrom(x => x.Content))
                .ForMember(opt => opt.Author, cfg => cfg.Ignore())
                .ForMember(x => x.PublshedOn, opt => opt.MapFrom(o => DateTime.UtcNow));

            CreateMap<Chapter, ChapterOutputModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Length, o => o.MapFrom(x => x.Content.Length))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.FanFictionUser.UserName))
                .ForMember(x => x.Title, o => o.MapFrom(x => x.Title ?? GlobalConstants.NoTitleAdded));

            CreateMap<Chapter, ChapterEditModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Length, o => o.MapFrom(x => x.Content.Length))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.FanFictionUser.UserName))
                .ForMember(x => x.Title, o => o.MapFrom(x => x.Title ?? GlobalConstants.NoTitleAdded))
                .ForMember(x => x.StoryId, o => o.MapFrom(x => x.FanFictionStoryId)).ReverseMap();

            CreateMap<ChapterInputModel, Chapter>()
                .ForMember(x => x.FanFictionStoryId, opt => opt.MapFrom(x => x.StoryId))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Comment, CommentOutputModel>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.FanFictionUser.UserName))
                .ForMember(x => x.CommentedOn, o => o.MapFrom(x => x.CommentedOn))
                .ForMember(x => x.Message, o => o.MapFrom(x => x.Message));

            CreateMap<CommentInputModel, Comment>()
                .ForMember(x => x.Message, o => o.MapFrom(x => x.Message))
                .ForMember(x => x.CommentedOn, o => o.MapFrom(x => x.CommentedOn))
                .ForMember(x => x.StoryId, o => o.MapFrom(x => x.StoryId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<StoryType, StoryTypeOutputModel>()
                .ForMember(x => x.Type, opt => opt.MapFrom(z => z.Name)).ReverseMap();

            CreateMap<Notification, NotificationOutputModel>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Message, o => o.MapFrom(x => x.Message))
                .ForMember(x => x.Seen, o => o.MapFrom(x => x.Seen))
                .ForMember(x => x.UpdatedStoryId, o => o.MapFrom(x => x.UpdatedStoryId))
                .ForMember(x => x.Username, o => o.MapFrom(x => x.FanFictionUser.UserName));

            CreateMap<Message, MessageOutputModel>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.IsReaden, o => o.MapFrom(x => x.IsReaden))
                .ForMember(x => x.Sender, o => o.MapFrom(x => x.Sender.UserName))
                .ForMember(x => x.Receiver, o => o.MapFrom(x => x.Receiver.UserName))
                .ForMember(x => x.SendOn, o => o.MapFrom(x => x.SendOn))
                .ForMember(x => x.Text, o => o.MapFrom(x => x.Text));
        }
    }
}