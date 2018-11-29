namespace FanFiction.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;

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

        public HomeLoggedModel GetHomeViewDetails()
        {
            var homeviewmodel = new HomeLoggedModel
            {
                Stories = this.Context.FictionStories.OrderByDescending(x => x.CreatedOn).Take(10).ProjectTo<StoryOutputModel>().ToList(),
                Announcements = this.Context.Announcements.OrderByDescending(x => x.PublshedOn).Take(10).ProjectTo<AnnouncementOutputModel>().ToList()
            };

            return homeviewmodel;
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
    }
}