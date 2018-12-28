namespace FanFictionApp.Views.ViewComponents
{
	using AutoMapper;
	using System.Linq;
	using FanFiction.Data;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore;
	using FanFiction.Services.Utilities;
	using AutoMapper.QueryableExtensions;
	using FanFiction.ViewModels.OutputModels.Chapters;

	[ViewComponent(Name = "ChapterList")]
	public class ChapterListViewComponent : ViewComponent
	{
		public ChapterListViewComponent(FanFictionContext context, IMapper mapper)
		{
			this.Context = context;
			this.Mapper = mapper;
		}

		protected IMapper Mapper { get; set; }

		protected FanFictionContext Context { get; }

		public async Task<IViewComponentResult> InvokeAsync(int storyId)
		{
			var chapters = await GetChaptersAsync(storyId);
			this.ViewData[GlobalConstants.StoryId] = storyId;
			return View(chapters);
		}

		private async Task<List<ChapterOutputModel>> GetChaptersAsync(int id)
		{
			var chapters = await this.Context.Chapters.Where(x => x.FanFictionStoryId == id).ProjectTo<ChapterOutputModel>(Mapper.ConfigurationProvider).ToListAsync();

			return chapters;
		}
	}
}