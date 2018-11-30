namespace FanFiction.Services
{
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
            FanFictionContext context, IMapper mapper)
            : base(userManager, signInManager, context, mapper)
        {
        }

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
    }
}