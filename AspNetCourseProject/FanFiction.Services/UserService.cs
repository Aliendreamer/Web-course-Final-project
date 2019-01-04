namespace FanFiction.Services
{
	using Data;
	using Models;
	using System;
	using Utilities;
	using Interfaces;
	using AutoMapper;
	using System.Linq;
	using System.Threading.Tasks;
	using ViewModels.InputModels;
	using ViewModels.OutputModels;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Identity;
	using ViewModels.OutputModels.Users;
	using Microsoft.EntityFrameworkCore;
	using AutoMapper.QueryableExtensions;
	using ViewModels.OutputModels.Stories;
	using ViewModels.OutputModels.Announcements;

	public class UserService : BaseService, IUserService
	{
		public UserService(SignInManager<FanFictionUser> signInManager, UserManager<FanFictionUser> userManager,
			FanFictionContext context,
			IMapper mapper)
			: base(userManager, context, mapper)
		{
			this.SignInManager = signInManager;
		}

		protected SignInManager<FanFictionUser> SignInManager { get; }

		public SignInResult LogUser(LoginInputModel loginModel)
		{
			var user = this.Context.Users.FirstOrDefault(x => x.Nickname == loginModel.Nickname);

			if (user == null)
			{
				return SignInResult.Failed;
			}

			var password = loginModel.Password;
			var result = this.SignInManager.PasswordSignInAsync(user, password, true, false).Result;

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
			await this.UserManager.AddToRoleAsync(user, GlobalConstants.DefaultRole);
			var result = await this.SignInManager.PasswordSignInAsync(user, registerModel.Password, true, false);

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
				.Take(2).ProjectTo<StoryHomeOutputModel>(Mapper.ConfigurationProvider).ToList(),

				Announcements = this.Context.Announcements
					.Where(x => x.PublshedOn.AddMonths(1) >= DateTime.Now.Date)
					.OrderByDescending(x => x.PublshedOn)
					.Take(3)
					.ProjectTo<AnnouncementOutputModel>(Mapper.ConfigurationProvider)
					.ToList()
			};

			return homeViewModel;
		}

		public IEnumerable<BlockedUserOutputModel> BlockedUsers(string userId)
		{
			var blockedUsers = this.Context.BlockedUsers
				.Where(x => x.FanfictionUserId == userId).Select(x => x.BlockedUser).ProjectTo<BlockedUserOutputModel>(Mapper.ConfigurationProvider).ToArray();

			return blockedUsers;
		}

		public async void Logout()
		{
			await this.SignInManager.SignOutAsync();
		}

		public UserOutputViewModel GetUser(string username)
		{
			var user = this.Context.Users
				.Include(x => x.FanFictionStories)
				.Include(x => x.FollowedStories)
				.ThenInclude(x => x.FanFictionStory)
				.Include(x => x.BlockedUsers)
				.Include(x => x.BLockedBy)
				.Include(x => x.Comments)
				.Include(x => x.Notifications)
				.Include(x => x.SendMessages)
				.Include(x => x.ReceivedMessages)
				.Include(x => x.StoryRatings)
				.FirstOrDefault(x => x.UserName == username);

			var result = Mapper.Map<UserOutputViewModel>(user);
			result.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

			return result;
		}

		public async Task BlockUser(string currentUser, string name)
		{
			var blockingUser = await this.UserManager.FindByNameAsync(currentUser);
			var userTobeBlocked = await this.UserManager.FindByNameAsync(name);

			var Blocked = new BlockedUsers
			{
				FanFictionUser = blockingUser,
				BlockedUser = userTobeBlocked
			};

			bool alreadyBlocked = this.Context.BlockedUsers.Any(x =>
				x.BlockedUser == userTobeBlocked && x.FanFictionUser == blockingUser);

			if (alreadyBlocked)
			{
				throw new InvalidOperationException(string.Format(GlobalConstants.AlreadyExistsInDb,
					typeof(BlockedUsers).Name));
			}

			this.Context.BlockedUsers.Add(Blocked);
			this.Context.SaveChanges();
		}

		public void UnblockUser(string userId, string id)
		{
			var blocked =
				this.Context.BlockedUsers.FirstOrDefault(x =>
					x.FanfictionUserId == userId && x.BlockedUserId == id);

			if (blocked == null)
			{
				throw new InvalidOperationException(GlobalConstants.NoRecordInDb);
			}

			this.Context.Remove(blocked);
			this.Context.SaveChanges();
		}
	}
}