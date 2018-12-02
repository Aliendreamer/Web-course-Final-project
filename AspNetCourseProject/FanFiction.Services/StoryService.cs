namespace FanFiction.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Utilities;
    using ViewModels.OutputModels.Stories;

    public class StoryService : BaseService, IStoryService
    {
        public StoryService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public ICollection<StoryOutputModel> CurrentStories()
        {
            var stories = this.Context.FictionStories.ProjectTo<StoryOutputModel>().ToArray();

            return stories;
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
    }
}