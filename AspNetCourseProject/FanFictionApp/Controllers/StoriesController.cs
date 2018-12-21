﻿namespace FanFictionApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using FanFiction.Services.Utilities;
    using FanFiction.Services.Interfaces;
    using FanFiction.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
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
            this.ViewData[GlobalConstants.UsernameHolder] = username;
            var userStories = this.StoryService.UserStories(username);
            return this.View(userStories);
        }

        [HttpGet]
        public IActionResult CreateStory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryInputModel inputModel)
        {
            var fileType = inputModel.StoryImage.ContentType.Split('/')[1];

            bool wrongType = fileType == GlobalConstants.JpgFormat || fileType == GlobalConstants.PngFormat;

            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (!wrongType)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.WrongFileType;
                return this.View(inputModel);
            }

            var id = await this.StoryService.CreateStory(inputModel);

            return RedirectToAction("Details", "Stories", new { id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var fictionStory = this.StoryService.GetStoryById(id);
            return this.View(fictionStory);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStory(int id)
        {
            string username = this.User.Identity.Name;

            await this.StoryService.DeleteStory(id, username);

            return RedirectToAction("UserStories", "Stories", new { username });
        }

        [HttpGet]
        public async Task<IActionResult> Follow(int id)
        {
            var username = this.User.Identity.Name;
            await this.StoryService.Follow(username, id);

            return RedirectToAction("Details", "Stories", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> UnFollow(int id)
        {
            var username = this.User.Identity.Name;
            await this.StoryService.UnFollow(username, id);

            return RedirectToAction("Details", "Stories", new { id });
        }

        [HttpPost]
        public IActionResult AddRating([FromForm]int storyId, [FromForm]double rating)
        {
            string username = this.User.Identity.Name;

            this.StoryService.AddRating(storyId, rating, username);

            return RedirectToAction("Details", "Stories", new { id = storyId });
        }
    }
}