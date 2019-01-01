namespace FanFictionApp.Extensions
{
	using System.Linq;
	using System.Threading.Tasks;
	using FanFiction.Models;
	using FanFiction.Services.Utilities;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;

	public class SeedRolesMiddleware
	{
		private readonly RequestDelegate _next;

		public SeedRolesMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context,
			UserManager<FanFictionUser> userManager
			, RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{
				await SeedRoles(userManager, roleManager);
			}

			// Call the next delegate/middleware in the pipeline
			await _next(context);
		}

		private async Task SeedRoles(
			UserManager<FanFictionUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.Admin
			});
			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.Moderator
			});

			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.PaidUser
			});

			var user = new FanFictionUser
			{
				UserName = "AppAdmin",
				Email = "admin@admin.com",
				Nickname = "ThatAdmin"
			};

			string adminPass = "admin";

			await userManager.CreateAsync(user, adminPass);

			await userManager.AddToRoleAsync(user, GlobalConstants.Admin);
		}
	}
}