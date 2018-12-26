namespace FanFictionApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using FanFiction.Services.Interfaces;
	using FanFiction.ViewModels.InputModels;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class CommentsController : Controller
	{
		public CommentsController(ICommentService commentService)
		{
			this.CommentService = commentService;
		}

		protected ICommentService CommentService { get; set; }

		[HttpGet]
		public IActionResult DeleteComment(int storyId, int id)
		{
			this.CommentService.DeleteComment(id);

			return RedirectToAction("Details", "Stories", new { id = storyId });
		}

		[HttpPost]
		public IActionResult AddComment(CommentInputModel inputModel)
		{
			this.CommentService.AddComment(inputModel);

			return RedirectToAction("Details", "Stories", new { id = inputModel.StoryId });
		}

		[HttpGet]
		public IActionResult DeleteCommentFromInfoHub(int id)
		{
			this.CommentService.DeleteComment(id);

			return RedirectToInfohub();
		}

		[HttpGet]
		public IActionResult DeleteAllComments(string username)
		{
			this.CommentService.DeleteAllComments(username);

			return RedirectToInfohub();
		}

		private IActionResult RedirectToInfohub()
		{
			var username = this.User.Identity.Name;

			return RedirectToAction("Infohub", "Messages", new { username });
		}
	}
}