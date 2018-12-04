namespace FanFiction.Services
{
    using System;
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
    using ViewModels.InputModels;
    using ViewModels.OutputModels;
    using ViewModels.OutputModels.Announcements;
    using ViewModels.OutputModels.Stories;
    using ViewModels.OutputModels.Users;

    public class UserService : BaseService, IUserService
    {
        public UserService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context,
            IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

        public SignInResult LogUser(LoginInputModel loginModel)
        {
            var user = this.Context.Users.FirstOrDefault(x => x.Nickname == loginModel.Nickname);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var password = loginModel.Password;
            var result = this.SingInManager.PasswordSignInAsync(user, password, true, false).Result;

            return result;
        }

        public async Task<SignInResult> RegisterUser(RegisterInputModel registerModel)
        {
            bool uniqueNickname = this.Context.Users.All(x => x.Nickname != registerModel.Nickname);

            if (!uniqueNickname)
            {
                return SignInResult.Failed;
            }

            var user = Mapper.Map<FanFictionUser>(registerModel);

            await this.UserManager.CreateAsync(user);
            await this.UserManager.AddPasswordAsync(user, registerModel.Password);
            var result = await this.SingInManager.PasswordSignInAsync(user, registerModel.Password, true, false);

            return result;
        }

        // because i didn't want to inject other services in the view
        public HomeLoggedModel GetHomeViewDetails()
        {
            var homeViewModel = new HomeLoggedModel
            {
                Stories = this.Context.FictionStories
                .Include(x => x.Ratings)
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedOn)
                .Take(2).ProjectTo<StoryHomeOutputModel>().ToList(),

                Announcements = this.Context.Announcements
                    .Where(x => x.PublshedOn.AddMonths(1) >= DateTime.Now.Date)
                    .OrderByDescending(x => x.PublshedOn)
                    .Take(3)
                    .ProjectTo<AnnouncementOutputModel>()
                    .ToList()
            };

            return homeViewModel;
        }

        public string GetUserNickname(string username)
        {
            var user = this.Context.Users.First(x => x.UserName == username);
            var nick = user.Nickname;

            return nick;
        }

        public async void Logout()
        {
            await this.SingInManager.SignOutAsync();
        }

        public UserOutputViewModel GetUser(string username)
        {
            //TODO try to see if i can do this with context.Users in one query? should be better and then automapper will work!probably
            var user = this.UserManager.FindByNameAsync(username).Result;
            var stories = this.Context.FictionStories.ProjectTo<StoryOutputModel>().ToArray();
            var users = this.Context.BlockedUsers.ToList();
            var result = Mapper.Map<UserOutputViewModel>(user);
            result.FollowedStories = stories.Where(x => x.Followers.Any(xz => xz.NickName == result.NickName)).ToList();
            result.UserStories = stories.Where(x => x.Author.NickName == result.NickName).ToList();
            result.Stories = result.UserStories.Count;
            result.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;
            result.BlockedUsers = users.Where(x => x.FanfictionUserId == user.Id).ToList().Count;
            result.BlockedBy = users.Where(x => x.BlockedUserId == user.Id).ToList().Count;
            return result;
        }
    }
}