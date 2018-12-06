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
    using Microsoft.EntityFrameworkCore.Migrations.Design;

    public class StoryService : BaseService, IStoryService
    {
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

        public async Task CreateStory(StoryInputModel inputModel)
        {
            var accloudinary = SetCloudinary();

            var url = await UploadImage(accloudinary, inputModel.StoryImage, inputModel.Title);

            var newStory = Mapper.Map<FanFictionStory>(inputModel);

            newStory.Author = await this.UserManager.FindByNameAsync(inputModel.Author);
            newStory.Type = this.Context.StoryTypes.First(x => x.Name == inputModel.Genre);
            newStory.ImageUrl = url ?? GlobalConstants.DefaultNoImage;

            this.Context.FictionStories.Add(newStory);
            await this.Context.SaveChangesAsync();
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
                File = new FileDescription(storyName, ms)
            };

            var uploadResult = cloudinary.Upload(uploadParams);
            var url = uploadResult.Uri.AbsolutePath;
            ms.Dispose();
            return url;
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