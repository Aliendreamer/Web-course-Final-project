namespace FanFictionApp.Controllers
{
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    public class StoriesController : Controller
    {
        public StoriesController(IStoryService storyService)
        {
            this.StoryService = storyService;
        }

        protected IStoryService StoryService { get; }

        [HttpGet]
        public IActionResult AllStories()
        {
            var model = this.StoryService.CurrentStories(null);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult AllStories(string type)
        {
            var model = this.StoryService.CurrentStories(type);
            return this.View(model);
        }

        [HttpGet]
        [Route(GlobalConstants.RouteConstants.UserStories)]
        public IActionResult UserStories(string username)
        {
            var userStories = this.StoryService.UserStories(username);
            return this.View(userStories);
        }

        [HttpGet]
        public IActionResult CreateStory()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateStory(StoryInputModel inputModel)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult StoryDetails()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddChapter()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteChapter()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult DeleteStory(int id)
        {
            return null;
        }

        public IActionResult Details()
        {
            throw new System.NotImplementedException();
        }
    }
}