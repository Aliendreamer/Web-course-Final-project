namespace FanFiction.Services
{
	using AutoMapper;
	using Data;
	using Microsoft.AspNetCore.Identity;
	using Models;

	public abstract class BaseService
	{
		//TODO: maybe move user manager in admin and user service only but leaving it for now!
		protected BaseService(UserManager<FanFictionUser> userManager,
			FanFictionContext context,
			IMapper mapper)
		{
			this.UserManager = userManager;

			this.Context = context;
			this.Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		protected FanFictionContext Context { get; }

		protected UserManager<FanFictionUser> UserManager { get; }
	}
}