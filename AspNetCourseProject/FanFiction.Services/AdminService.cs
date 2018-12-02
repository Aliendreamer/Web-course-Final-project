namespace FanFiction.Services
{
    using System;
    using System.Collections.Generic;
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
    using ViewModels.OutputModels.Announcements;
    using ViewModels.OutputModels.Stories;
    using ViewModels.OutputModels.Users;

    public class AdminService : BaseService, IAdminService
    {
        public AdminService(UserManager<FanFictionUser> userManager,
            SignInManager<FanFictionUser> signInManager,
            FanFictionContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
            : base(userManager, signInManager, context, mapper)
        {
            this.RoleManager = roleManager;
        }

        protected RoleManager<IdentityRole> RoleManager { get; }

        public async Task<IEnumerable<UserAdminViewModel>> AllUsers()
        {
            var users = this.Context.Users.Include(x => x.FanFictionStories)
                .Include(x => x.Comments)
                .Include(x => x.SendMessages)
                .Include(x => x.ReceivedMessages)
                .ToList();

            var modelUsers = users.AsQueryable().ProjectTo<UserAdminViewModel>().ToList();

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

            var result = this.UserManager.DeleteAsync(user).Result;

            if (result != IdentityResult.Success)
            {
                throw new ArgumentException(GlobalConstants.ErrorOnDeleteUser);
            }
            await this.Context.SaveChangesAsync();
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
            var result = this.Context.Announcements.ProjectTo<AnnouncementOutputModel>().ToArray();
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

        public ICollection<StoryTypeOutputModel> Genres()
        {
            return this.Context.StoryTypes.ProjectTo<StoryTypeOutputModel>().ToArray();
        }

        private ICollection<string> AppRoles()
        {
            var result = this.RoleManager.Roles.Select(x => x.Name).ToArray();

            return result;
        }
    }
}