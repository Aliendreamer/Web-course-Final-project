namespace FanFiction.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Utilities;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Stories;
    using Microsoft.AspNetCore.Http;

    public class StoryService : BaseService, IStoryService
    {
        //TODO: I should probably refactor this and Create ChapterService? probably?
        public StoryService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public ICollection<StoryOutputModel> CurrentStories(string type = null)
        {
            if (string.IsNullOrEmpty(type) || type == GlobalConstants.ReturnAllStories)
            {
                return this.Context.FictionStories.ProjectTo<StoryOutputModel>().ToArray();
            }
            var stories = this.Context.FictionStories.Where(x => x.Type.Name == type).ProjectTo<StoryOutputModel>().ToArray();

            return stories;
        }

        public ICollection<StoryOutputModel> UserStories(string username)
        {
            var user = this.UserManager.Users.FirstOrDefault(x => x.UserName == username);
            var userStories = this.Context.FictionStories.Where(x => x.Author.Nickname == user.Nickname)
                .ProjectTo<StoryOutputModel>().ToArray();

            return userStories;
        }

        public ICollection<StoryTypeOutputModel> Genres()
        {
            return this.Context.StoryTypes.ProjectTo<StoryTypeOutputModel>().ToArray();
        }

        public async Task Follow(string username, int id)
        {
            var user = await this.UserManager.FindByNameAsync(username);

            var userstory = new UserStory
            {
                FanFictionStoryId = id,
                FanfictionUserId = user.Id
            };

            this.Context.UsersStories.Add(userstory);
            await this.Context.SaveChangesAsync();
        }

        public async Task UnFollow(string username, int id)
        {
            var user = await this.UserManager.FindByNameAsync(username);
            var entity = this.Context.UsersStories
                .Where(st => st.FanFictionStoryId == id)
                .Select(st => new UserStory
                {
                    FanFictionStoryId = id,
                    FanfictionUserId = user.Id
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
            var entity = this.Context.UsersStories
                .Where(st => st.FanFictionStoryId == id)
                .Select(st => new UserStory
                {
                    FanFictionStoryId = id,
                    FanfictionUserId = userId
                }).FirstOrDefault();

            var result = entity != null;

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

        public StoryDetailsOutputModel GetStoryById(int id)
        {
            var story = this.Context.FictionStories.Include(x => x.Type).Include(x => x.Author).FirstOrDefault(x => x.Id == id);

            if (story == null)
            {
                throw new ArgumentException(GlobalConstants.MissingStory);
            }

            var storyModel = this.Mapper.Map<StoryDetailsOutputModel>(story);

            return storyModel;
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
                throw new InvalidOperationException(string.Join(GlobalConstants.NotValidChapterStoryConnection, story.Id, chapter.Id));
            }
            this.Context.Chapters.Remove(chapter);
            this.Context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void AddChapter(ChapterInputModel inputModel)
        {
            var user = this.UserManager.FindByNameAsync(inputModel.Author).GetAwaiter().GetResult();
            var chapter = Mapper.Map<Chapter>(inputModel);
            chapter.AuthorId = user.Id;

            this.Context.Chapters.Add(chapter);
            this.Context.SaveChanges();
        }

        public async Task DeleteStory(int id, string username)
        {
            var story = this.Context.FictionStories.Include(x => x.Author).Include(x => x.Chapters).FirstOrDefaultAsync(x => x.Id == id).Result;
            var user = await this.UserManager.FindByNameAsync(username);
            var roles = await this.UserManager.GetRolesAsync(user);

            bool hasRights = roles.All(x => x != GlobalConstants.Admin || x != GlobalConstants.Moderator);
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