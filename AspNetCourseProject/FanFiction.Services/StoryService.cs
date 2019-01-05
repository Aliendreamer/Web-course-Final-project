namespace FanFiction.Services
{
	using Data;
	using Models;
	using System;
	using Utilities;
	using System.IO;
	using Interfaces;
	using AutoMapper;
	using System.Linq;
	using CloudinaryDotNet;
	using System.Threading.Tasks;
	using ViewModels.InputModels;
	using CloudinaryDotNet.Actions;
	using Microsoft.AspNetCore.Http;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using AutoMapper.QueryableExtensions;
	using ViewModels.OutputModels.Stories;

	public class StoryService : BaseService, IStoryService
	{
		public StoryService(INotificationService notificationService, UserManager<FanFictionUser> userManager,
			FanFictionContext context, IMapper mapper)
			: base(userManager, context, mapper)
		{
			this.NotificationService = notificationService;
		}

		protected INotificationService NotificationService { get; }

		public ICollection<StoryOutputModel> CurrentStories(string type = null)
		{
			if (string.IsNullOrEmpty(type) || type == GlobalConstants.ReturnAllStories)
			{
				return this.Context.FictionStories.ProjectTo<StoryOutputModel>(Mapper.ConfigurationProvider).ToArray();
			}
			var stories = this.Context.FictionStories.Where(x => string.Equals(x.Type.Name, type, StringComparison.CurrentCultureIgnoreCase))
				.ProjectTo<StoryOutputModel>(Mapper.ConfigurationProvider).ToArray();

			return stories;
		}

		public ICollection<StoryOutputModel> UserStories(string id)
		{
			var userStories = this.Context.FictionStories.Include(x => x.Author)
				.Where(x => x.Author.Id == id)
				.ProjectTo<StoryOutputModel>(Mapper.ConfigurationProvider).ToArray();

			return userStories;
		}

		public ICollection<StoryTypeOutputModel> Genres()
		{
			return this.Context.StoryTypes.ProjectTo<StoryTypeOutputModel>(Mapper.ConfigurationProvider).ToArray();
		}

		public async Task Follow(string username, string userId, int id)
		{
			var userstory = new UserStory
			{
				FanFictionStoryId = id,
				FanfictionUserId = userId
			};

			bool followed = IsFollowed(userId, id);
			if (followed)
			{
				throw new InvalidOperationException(string.Join(GlobalConstants.UserFollowAlready, username));
			}

			this.Context.UsersStories.Add(userstory);
			await this.Context.SaveChangesAsync();
		}

		public async Task UnFollow(string username, int storyId)
		{
			var entity = this.Context.UsersStories
				.Where(st => st.FanFictionStoryId == storyId)
				.Select(st => new UserStory
				{
					FanFictionStoryId = storyId,
					FanfictionUserId = username
				}).FirstOrDefault();

			if (entity != null)
			{
				this.Context.UsersStories.Remove(entity);
				await this.Context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentNullException(GlobalConstants.UserDontFollow);
			}
		}

		public bool IsFollowed(string userId, int id)
		{
			var result = this.Context.UsersStories
				.Any(x => x.FanfictionUserId == userId && x.FanFictionStoryId == id);

			return result;
		}

		public async Task<int> CreateStory(StoryInputModel inputModel)
		{
			var accloudinary = SetCloudinary();

			var url = await UploadImage(accloudinary, inputModel.StoryImage, inputModel.Title);

			var newStory = Mapper.Map<FanFictionStory>(inputModel);

			newStory.Author = await this.UserManager.FindByNameAsync(inputModel.Author);
			newStory.Type = this.Context.StoryTypes.First(x => x.Name == inputModel.Genre);
			newStory.ImageUrl = url ?? GlobalConstants.DefaultNoImage;

			this.Context.FictionStories.Add(newStory);
			await this.Context.SaveChangesAsync();
			return newStory.Id;
		}

		public ICollection<StoryOutputModel> FollowedStories(string username)
		{
			var stories = this.Context.FictionStories
				.Include(x => x.Followers)
				.Where(x => x.Followers.Any(z => z.FanFictionUser.UserName == username))
				.ProjectTo<StoryOutputModel>(Mapper.ConfigurationProvider).ToList();

			return stories;
		}

		public ICollection<StoryOutputModel> FollowedStoriesByGenre(string username, string type)
		{
			var stories = this.FollowedStories(username);

			if (type == GlobalConstants.ReturnAllStories)
			{
				return stories;
			}
			return stories.Where(x => x.Type.Type == type).ToList();
		}

		public StoryDetailsOutputModel GetStoryById(int id)
		{
			var story = this.Context.FictionStories
				.Include(x => x.Type)
				.Include(x => x.Chapters)
				.Include(x => x.Comments)
				.Include(x => x.Author)
				.Include(x => x.Ratings)
				.ThenInclude(z => z.StoryRating)
				.FirstOrDefault(x => x.Id == id);

			if (story == null)
			{
				throw new ArgumentException(GlobalConstants.MissingStory);
			}

			var storyModel = this.Mapper.Map<StoryDetailsOutputModel>(story);

			return storyModel;
		}

		public void AddRating(int storyId, double rate, string username)
		{
			var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();
			var story = this.Context.FictionStories.Find(storyId);

			bool rated = AlreadyRated(story.Id, user.UserName);

			if (rated)
			{
				throw new InvalidOperationException(GlobalConstants.AlreadyRated);
			}

			var rating = new StoryRating
			{
				Rating = rate,
				UserId = user.Id
			};
			this.Context.StoryRatings.Add(rating);

			var storyRating = new FanFictionRating
			{
				FanFictionId = storyId,
				RatingId = rating.Id
			};
			this.Context.FictionRatings.Add(storyRating);
			story.Ratings.Add(storyRating);
			this.Context.Update(story);
			this.Context.SaveChanges();
		}

		public bool AlreadyRated(int storyId, string username)
		{
			var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

			var rated = this.Context.FictionRatings.Any(x => x.FanFictionId == storyId && x.StoryRating.UserId == user.Id);

			return rated;
		}

		public async Task DeleteStory(int id, string username)
		{
			var story = this.Context.FictionStories.Include(x => x.Author).Include(x => x.Chapters).FirstOrDefaultAsync(x => x.Id == id).Result;
			var user = await this.UserManager.FindByNameAsync(username);
			var roles = await this.UserManager.GetRolesAsync(user);

			bool hasRights = roles.Any(x => x == GlobalConstants.Admin || x == GlobalConstants.Moderator);
			bool author = user.Nickname == story?.Author.Nickname;

			if (!hasRights && !author)
			{
				throw new OperationCanceledException(GlobalConstants.UserLackRights);
			}

			this.Context.FictionStories.Remove(story ?? throw new InvalidOperationException(GlobalConstants.NoRecordInDb));
			this.Context.SaveChanges();
		}

		private async Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileform, string storyName)
		{
			if (fileform == null)
			{
				return null;
			}

			byte[] storyImage;

			using (var memoryStream = new MemoryStream())
			{
				await fileform.CopyToAsync(memoryStream);
				storyImage = memoryStream.ToArray();
			}

			var ms = new MemoryStream(storyImage);

			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(storyName, ms),
				Transformation = new Transformation().Width(200).Height(250).Crop("fit").SetHtmlWidth(250).SetHtmlHeight(100)
			};

			var uploadResult = cloudinary.Upload(uploadParams);

			ms.Dispose();
			return uploadResult.SecureUri.AbsoluteUri;
		}

		private Cloudinary SetCloudinary()
		{
			Account account = new Account(
				GlobalConstants.CLoudinarySetup.CloudinaryCloudName,
				GlobalConstants.CLoudinarySetup.AccCloudinaryApiKey,
				GlobalConstants.CLoudinarySetup.AccCloudinarySecret);

			Cloudinary cloudinary = new Cloudinary(account);

			return cloudinary;
		}
	}
}