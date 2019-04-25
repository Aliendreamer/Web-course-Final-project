namespace FanFictionApp
{
	using System;
	using System.Linq;
	using FanFiction.Data;
	using FanFiction.Models;
	using Microsoft.AspNetCore;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;

	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var serverProvider = scope.ServiceProvider;

				SeedStoryTypesIfDbEmpty(serverProvider).GetAwaiter().GetResult();
			}
			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
				

		private static async Task SeedStoryTypesIfDbEmpty(IServiceProvider serviceProvider)
		{
			var dbContext = serviceProvider.GetRequiredService<FanFictionContext>();
		
			var storytypes = new[]
			{
				new StoryType
				{
					Name = "fantasy"
				},
				new StoryType
				{
					Name = "horror"
				},
				new StoryType
				{
					Name = "science fiction"
				},
			};

			var noGenres = dbContext.StoryTypes.Any();
			if (!noGenres)
			{
				await dbContext.StoryTypes.AddRangeAsync(storytypes);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}