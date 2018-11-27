namespace FanFiction.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using ViewModels.InputModels;

    public class UserService : BaseService, IUserService
    {
        public UserService(UserManager<FanFictionUser> userManager, SignInManager<FanFictionUser> signInManager, FanFictionContext context)
            : base(userManager, signInManager, context)
        {
        }

        public async Task<SignInResult> LogUser(LoginInputModel loginModel)
        {
            var user = this.Context.Users.First(x => x.Nickname == loginModel.Nickname);
            var password = loginModel.Password;
            var result = await this.SingInManager.PasswordSignInAsync(user, password, true, false);

            return result;
        }
    }
}