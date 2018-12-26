namespace FanFictionApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using FanFiction.Services.Utilities;
	using FanFiction.Services.Interfaces;
	using FanFiction.ViewModels.InputModels;
	using Microsoft.AspNetCore.Authorization;
	using FanFiction.ViewModels.OutputModels.Chapters;

	[Authorize]
	public class ChaptersController : Controller
	{
		public ChaptersController(IChapterService chapterService)
		{
			this.ChapterService = chapterService;
		}

		public IChapterService ChapterService { get; }

		[HttpPost]
		public IActionResult AddChapter(ChapterInputModel inputModel)
		{
			if (!ModelState.IsValid)
			{
				this.ViewData[GlobalConstants.ChapterLength] = inputModel.Content?.Length ?? 0;
				this.ViewData[GlobalConstants.StoryId] = inputModel.StoryId;
				return this.View(inputModel);
			}

			this.ChapterService.AddChapter(inputModel);

			return RedirectToAction("Details", "Stories", new { id = inputModel.StoryId });
		}

		[HttpGet]
		[Route(GlobalConstants.RouteConstants.AddChapterRoute)]
		public IActionResult AddChapter(int storyId)
		{
			this.ViewData[GlobalConstants.StoryId] = storyId;
			return this.View();
		}

		[HttpGet]
		public IActionResult DeleteChapter([FromQuery]int storyId, [FromQuery] int chapterid)
		{
			string username = this.User.Identity.Name;
			this.ChapterService.DeleteChapter(storyId, chapterid, username);

			this.ViewData[GlobalConstants.RedirectAfterAction] = storyId;
			return RedirectToAction("Details", "Stories", new { id = storyId });
		}

		[HttpGet]
		public IActionResult EditChapter(int id)
		{
			var model = this.ChapterService.GetChapterToEditById(id);

			if (model == null)
			{
				return NotFound();
			}

			return this.View(model);
		}

		[HttpPost]
		public IActionResult EditChapter(ChapterEditModel editModel)
		{
			if (!ModelState.IsValid)
			{
				this.ViewData[GlobalConstants.ChapterLength] = editModel.Content?.Length ?? 0;
				return this.View(editModel);
			}

			this.ChapterService.EditChapter(editModel);

			return RedirectToAction("Details", "Stories", new { id = editModel.StoryId });
		}
	}
}