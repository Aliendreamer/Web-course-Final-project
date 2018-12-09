namespace FanFictionApp.Views.ViewComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using FanFiction.Data;
    using FanFiction.ViewModels.OutputModels.Stories;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    [ViewComponent(Name = "ChapterList")]
    public class ChapterListViewComponent : ViewComponent
    {
        public ChapterListViewComponent(FanFictionContext context)
        {
            this.Context = context;
        }

        protected FanFictionContext Context { get; }

        public async Task<IViewComponentResult> InvokeAsync(int storyId)
        {
            var chapters = await GetChaptersAsync(storyId);
            this.ViewData["storyId"] = storyId;
            return View(chapters);
        }

        private async Task<List<ChapterOutputModel>> GetChaptersAsync(int id)
        {
            var chapters = await this.Context.Chapters.Where(x => x.FanFictionStoryId == id).ProjectTo<ChapterOutputModel>().ToListAsync();

            return chapters;
        }
    }
}