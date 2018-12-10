namespace FanFictionApp.Controllers
{
    using FanFiction.Services.Interfaces;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;

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
    }
}