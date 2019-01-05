namespace FanFiction.Services
{
	using Data;
	using Models;
	using System;
	using Utilities;
	using Interfaces;
	using AutoMapper;
	using System.Linq;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using AutoMapper.QueryableExtensions;
	using ViewModels.OutputModels.ApiOutputModels;
	using ViewModels.OutputModels.Stories;

	public class ApiService : BaseService, IApiService
	{
		public ApiService(UserManager<FanFictionUser> userManager, FanFictionContext context, IMapper mapper)
			: base(userManager, context, mapper)
		{
		}

		public IEnumerable<ApiUserOutputModel> Authors()
		{
			var result = this.Context.Users
				.Include(x => x.FanFictionStories)
				.Include(x => x.Chapters)
				.Where(x => x.FanFictionStories.Any())
				.ProjectTo<ApiUserOutputModel>(Mapper.ConfigurationProvider)
				.ToList();

			return result;
		}

		public IEnumerable<ApiFanFictionStoryOutputModel> Stories()
		{
			var result = this.Context.FictionStories
				.Include(x => x.Chapters)
				.Include(x => x.Ratings)
				.ProjectTo<ApiFanFictionStoryOutputModel>(Mapper.ConfigurationProvider)
				.ToList();

			return result;
		}

		public IEnumerable<ApiFanFictionStoryOutputModel> StoriesByGenre(string genre)
		{
			var genresFromdb = this.Context.StoryTypes.Select(x => x.Name).ToList();

			bool genreNone = genresFromdb.All(x => x.ToLower() != genre.ToLower());

			if (genreNone)
			{
				throw new ArgumentException(string.Join(GlobalConstants.NoSuchGenre, genre));
			}

			var result = this.Context.FictionStories
				.Include(x => x.Chapters)
				.Include(x => x.Ratings)
				.Include(x => x.Type)
				.Where(x => x.Type.Name == genre)
				.ProjectTo<ApiFanFictionStoryOutputModel>(Mapper.ConfigurationProvider)
				.ToList();

			return result;
		}

		public IEnumerable<ApiFanFictionStoryOutputModel> TopStories()
		{
			const int countTotake = 10;

			var result = this.Context.FictionStories
				.Include(x => x.Chapters)
				.Include(x => x.Ratings)
				.ProjectTo<ApiFanFictionStoryOutputModel>(Mapper.ConfigurationProvider)
				.OrderByDescending(x => x.Rating)
				.Take(countTotake)
				.ToList();

			return result;
		}
	}
}