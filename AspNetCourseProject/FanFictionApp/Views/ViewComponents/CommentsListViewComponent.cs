namespace FanFictionApp.Views.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using FanFiction.Data;
    using FanFiction.ViewModels.OutputModels.Stories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CommentsListViewComponent : ViewComponent
    {
        public CommentsListViewComponent(FanFictionContext context)
        {
            this.Context = context;
        }

        protected FanFictionContext Context { get; }

        public async Task<IViewComponentResult> InvokeAsync(int storyId)
        {
            var comments = await GetChaptersAsync(storyId);
            this.ViewData[FanFiction.Services.Utilities.GlobalConstants.StoryId] = storyId;
            return View(comments);
        }

        private async Task<List<CommentOutputModel>> GetChaptersAsync(int id)
        {
            var comments = await this.Context.Comments.Where(x => x.FanFictionStory.Id == id).ProjectTo<CommentOutputModel>().ToListAsync();

            return comments;
        }
    }
}