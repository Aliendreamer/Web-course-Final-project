namespace FanFiction.Services
{
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public abstract class BaseService
    {
        protected BaseService(UserManager<FanFictionUser> userManager, SignInManager<FanFictionUser> signInManager, FanFictionContext context, IMapper mapper)
        {
            this.UserManager = userManager;
            this.SingInManager = signInManager;
            this.Context = context;
            this.Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected FanFictionContext Context { get; }

        protected SignInManager<FanFictionUser> SingInManager { get; }

        protected UserManager<FanFictionUser> UserManager { get; }
    }
}