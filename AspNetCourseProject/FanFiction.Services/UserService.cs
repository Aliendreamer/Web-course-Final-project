namespace FanFiction.Services
{
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class UserService : BaseService, IUserService
    {
        public UserService(UserManager<FanFictionUser> userManager, SignInManager<FanFictionUser> signInManager, FanFictionContext context)
            : base(userManager, signInManager, context)
        {
        }
    }
}