﻿namespace FanFiction.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Utilities;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Chapters;
    using AutoMapper.QueryableExtensions;

    public class ChapterService : BaseService, IChapterService
    {
        public ChapterService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper, INotificationService notificationService)
            : base(userManager, signInManager, context, mapper)
        {
            this.NotificationService = notificationService;
        }

        public INotificationService NotificationService { get; }

        public ChapterEditModel GetChapterToEditById(int id)
        {
            var result = this.Context.Chapters
                .Include(x => x.FanFictionUser)
                .Include(x => x.FanFictionStory).ProjectTo<ChapterEditModel>()
                .FirstOrDefault(x => x.Id == id);

            return result;
        }

        public void DeleteChapter(int storyId, int chapterid, string username)
        {
            var story = this.Context.FictionStories.Find(storyId);
            var chapter = this.Context.Chapters.Find(chapterid);

            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var userRoles = UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            bool roles = userRoles.All(x => x != GlobalConstants.Admin) ||
                         userRoles.All(x => x != GlobalConstants.Moderator);

            bool author = user.Id == chapter.AuthorId;

            if (roles && !author)
            {
                throw new InvalidOperationException(GlobalConstants.UserHasNoRights);
            }

            if (!author)
            {
                throw new InvalidOperationException(GlobalConstants.NotAuthor);
            }

            if (story.Chapters.All(x => x.Id != chapter.Id) || chapter.FanFictionStoryId != story.Id)
            {
                throw new InvalidOperationException(string.Join(GlobalConstants.NotValidChapterStoryConnection,
                    story.Id, chapter.Id));
            }

            this.Context.Chapters.Remove(chapter);
            this.Context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void AddChapter(ChapterInputModel inputModel)
        {
            var user = this.UserManager.FindByNameAsync(inputModel.Author).GetAwaiter().GetResult();
            var chapter = Mapper.Map<Chapter>(inputModel);
            chapter.AuthorId = user.Id;
            var story = this.Context.FictionStories.Find(inputModel.StoryId);
            story.LastEditedOn = inputModel.CreatedOn;

            this.Context.FictionStories.Update(story);
            this.Context.Chapters.Add(chapter);
            this.Context.SaveChanges();

            this.NotificationService.AddNotification(inputModel.StoryId, inputModel.Author, story.Title);
        }

        public void EditChapter(ChapterEditModel editModel)
        {
            var chapter = this.Context.Chapters.Find(editModel.Id);

            chapter.Content = editModel.Content;
            chapter.Title = editModel.Title;
            chapter.CreatedOn = editModel.CreatedOn;

            this.Context.Chapters.Update(chapter);
            this.Context.SaveChanges();
        }
    }
}