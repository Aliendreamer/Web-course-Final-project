namespace FanFiction.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using ViewModels.InputModels;

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

        public async void Logout()
        {
            await this.SingInManager.SignOutAsync();
        }
    }
}