namespace FanFiction.Services
{
	using Data;
	using Models;
	using System;
	using Utilities;
	using AutoMapper;
	using Interfaces;
	using System.Linq;
	using ViewModels.InputModels;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using AutoMapper.QueryableExtensions;
	using ViewModels.OutputModels.Chapters;

	public class ChapterService : BaseService, IChapterService
	{
		public ChapterService(UserManager<FanFictionUser> userManager,
			FanFictionContext context, IMapper mapper, INotificationService notificationService)
			: base(userManager, context, mapper)
		{
			this.NotificationService = notificationService;
		}

		public INotificationService NotificationService { get; }

		public ChapterEditModel GetChapterToEditById(int id)
		{
			var result = this.Context.Chapters
				.Include(x => x.FanFictionUser)
				.Include(x => x.FanFictionStory).ProjectTo<ChapterEditModel>(Mapper.ConfigurationProvider)
				.FirstOrDefault(x => x.Id == id);

			return result;
		}

		public void DeleteChapter(int storyId, int chapterid, string username)
		{
			var story = this.Context.FictionStories.Find(storyId);
			var chapter = this.Context.Chapters.Find(chapterid);

			var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

			var userRoles = UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

			bool admin = userRoles.Any(x => x == GlobalConstants.Admin);
			bool moderator = userRoles.Any(x => x == GlobalConstants.Moderator);
			bool author = user.Id == chapter.AuthorId;

			if (!admin && !moderator && !author)
			{
				throw new InvalidOperationException(GlobalConstants.UserHasNoRights + " " + GlobalConstants.NotAuthor);
			}

			bool nochapterInStory = story.Chapters.All(x => x.Id != chapter.Id);
			bool storyIdForThischapter = chapter.FanFictionStoryId != story.Id;

			if (nochapterInStory || storyIdForThischapter)
			{
				throw new ArgumentException(string.Join(GlobalConstants.NotValidChapterStoryConnection,
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