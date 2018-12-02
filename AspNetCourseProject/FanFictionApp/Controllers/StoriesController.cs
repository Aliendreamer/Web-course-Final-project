namespace FanFictionApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StoriesController : Controller
    {
        [HttpGet]
        public IActionResult AllStories()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult UserStories()
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
            return this.AllStories();
        }

        public IActionResult Details()
        {
            throw new System.NotImplementedException();
        }
    }
}