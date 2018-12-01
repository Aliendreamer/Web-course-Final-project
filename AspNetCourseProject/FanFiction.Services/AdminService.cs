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

        public ChangingRoleModel AdminModifyRole(string Id)
        {
            var user = this.UserManager.FindByIdAsync(Id).Result;

            var model = this.Mapper.Map<ChangingRoleModel>(user);

            model.AppRoles = this.AppRoles();

            model.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

            return model;
        }

        private ICollection<string> AppRoles()
        {
            var result = this.RoleManager.Roles.Select(x => x.Name).ToArray();

            return result;
        }
    }
}