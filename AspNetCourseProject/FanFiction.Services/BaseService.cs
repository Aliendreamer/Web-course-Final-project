namespace FanFiction.Services
{
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public abstract class BaseService
    {
        protected BaseService(UserManager<FanFictionUser> userManager, SignInManager<FanFictionUser> signInManager, FanFictionContext context)
        {
            this.UserManager = userManager;
            this.SingInManager = signInManager;
            this.Context = context;
        }

        protected FanFictionContext Context { get; set; }

        protected SignInManager<FanFictionUser> SingInManager { get; set; }

        protected UserManager<FanFictionUser> UserManager { get; set; }
    }
}