namespace FanFiction.Services
{
	using Data;
	using Models;
	using System;
	using Utilities;
	using Interfaces;
	using AutoMapper;
	using System.Linq;
	using ViewModels.InputModels;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Identity;
	using ViewModels.OutputModels.Users;
	using Microsoft.EntityFrameworkCore;
	using AutoMapper.QueryableExtensions;
	using ViewModels.OutputModels.Announcements;

	public class AdminService : BaseService, IAdminService
	{
		public AdminService(UserManager<FanFictionUser> userManager,
			FanFictionContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
			: base(userManager, context, mapper)
		{
			this.RoleManager = roleManager;
		}

		protected RoleManager<IdentityRole> RoleManager { get; }

		public async Task<IEnumerable<UserAdminViewOutputModel>> AllUsers()
		{
			var users = this.Context.Users
		   .Include(x => x.FanFictionStories)
		   .Include(x => x.FollowedStories)
		   .Include(x => x.Comments)
		   .Include(x => x.SendMessages)
		   .Include(x => x.ReceivedMessages)
		   .ToList();

			var modelUsers = Mapper.Map<IList<UserAdminViewOutputModel>>(users);

			for (int i = 0; i < users.Count; i++)
			{
				var current = users[i];
				var role = await this.UserManager.GetRolesAsync(current);
				modelUsers[i].Role = role.FirstOrDefault() ?? GlobalConstants.DefaultRole;
			}

			return modelUsers;
		}

		public async Task DeleteUser(string Id)
		{
			var user = await this.UserManager.FindByIdAsync(Id);

			try
			{
				await RemovingEntitiesFromDbByUserToDeleteId(user.Id);
				await this.UserManager.DeleteAsync(user);
				await this.Context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw new ArgumentException(GlobalConstants.ErrorOnDeleteUser);
			}
		}

		public void DeleteAnnouncement(int id)
		{
			var announce = this.Context.Announcements.Find(id);
			this.Context.Remove(announce);
			this.Context.SaveChanges();
		}

		public void DeleteAllAnnouncements()
		{
			var allAnounces = this.Context.Announcements.ToList();
			this.Context.RemoveRange(allAnounces);
			this.Context.SaveChanges();
		}

		public string AddGenre(string newType)
		{
			bool notExisting = this.Context.StoryTypes.Select(x => x.Name).All(x => x != newType);

			if (notExisting)
			{
				var newGenre = new StoryType
				{
					Name = newType
				};
				this.Context.StoryTypes.Add(newGenre);
				this.Context.SaveChanges();

				return GlobalConstants.Success;
			}

			return GlobalConstants.Failed;
		}

		public async Task<IdentityResult> ChangeRole(ChangingRoleModel model)
		{
			bool roleExist = this.RoleManager.RoleExistsAsync(model.NewRole).Result;

			var user = this.UserManager.FindByIdAsync(model.Id).Result;
			var currentRole = await this.UserManager.GetRolesAsync(user);
			IdentityResult result = null;

			if (!roleExist && !currentRole.Any())
			{
				return IdentityResult.Failed();
			}
			if (!roleExist && currentRole.Any())
			{
				result = await this.UserManager.RemoveFromRoleAsync(user, currentRole.First());
				return IdentityResult.Success;
			}
			if (roleExist && !currentRole.Any())
			{
				result = await this.UserManager.AddToRoleAsync(user, model.NewRole);
			}
			else
			{
				result = await this.UserManager.RemoveFromRoleAsync(user, currentRole.First());
				result = await this.UserManager.AddToRoleAsync(user, model.NewRole);
			}

			return result;
		}

		public AllAnnouncementsModel AllAnnouncements()
		{
			var result = this.Context.Announcements.ProjectTo<AnnouncementOutputModel>(Mapper.ConfigurationProvider).ToArray();
			var model = new AllAnnouncementsModel
			{
				Announcements = result,
				Announcement = new AnnouncementInputModel()
			};
			return model;
		}

		public ChangingRoleModel AdminModifyRole(string Id)
		{
			var user = this.UserManager.FindByIdAsync(Id).Result;

			var model = this.Mapper.Map<ChangingRoleModel>(user);

			model.AppRoles = this.AppRoles();

			model.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

			return model;
		}

		public void AddAnnouncement(AnnouncementInputModel inputModel)
		{
			var user = this.UserManager.FindByNameAsync(inputModel.Author).Result;

			var announce = Mapper.Map<Announcement>(inputModel);
			announce.Author = user;

			this.Context.Announcements.Add(announce);

			this.Context.SaveChanges();
		}

		private ICollection<string> AppRoles()
		{
			var result = this.RoleManager.Roles.Select(x => x.Name).ToArray();

			return result;
		}

		private async Task RemovingEntitiesFromDbByUserToDeleteId(string id)
		{
			var blockedEntitiesWithUserId =
				this.Context.BlockedUsers.Where(x => x.BlockedUserId == id || x.BlockedUserId == id);

			var notifications = this.Context.Notifications.Where(x => x.FanFictionUserId == id);
			var messages = this.Context.Messages.Where(x => x.ReceiverId == id || x.SenderId == id);

			this.Context.Notifications.RemoveRange(notifications);
			this.Context.Messages.RemoveRange(messages);
			this.Context.BlockedUsers.RemoveRange(blockedEntitiesWithUserId);
			await this.Context.SaveChangesAsync();
		}
	}
}