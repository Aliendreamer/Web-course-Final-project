namespace FanFictionApp.Areas.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using FanFiction.Services.Utilities;
	using FanFiction.Services.Interfaces;
	using FanFiction.ViewModels.OutputModels.ApiOutputModels;

	[Route(GlobalConstants.RouteConstants.ApiRoute)]
	[ApiController]
	public class FanFictionController : ControllerBase
	{
		//this is weird why i need to reference the list and can't send it directly very weird
		//should investigate!
		// return Ok(object) seems to do the trick strange
		//removed the async it was problematic with the service pretty weird IQuerable exceptions object set to null

		public FanFictionController(IApiService apiService)
		{
			this.ApiService = apiService;
		}

		protected IApiService ApiService { get; set; }

		[HttpGet]
		[Route(GlobalConstants.RouteConstants.Authors)]
		public ActionResult<IEnumerable<ApiUserOutputModel>> Authors()
		{
			var authors = this.ApiService.Authors();
			return new List<ApiUserOutputModel>(authors);
		}

		// GET: api/Stories
		[HttpGet]
		[Route(GlobalConstants.RouteConstants.Stories)]
		public ActionResult<IEnumerable<ApiFanFictionStoryOutputModel>> Stories()
		{
			var stories = this.ApiService.Stories();

			//return Ok(stories);
			return new List<ApiFanFictionStoryOutputModel>(stories);
		}

		// GET: api/TopStories
		[HttpGet]
		[Route(GlobalConstants.RouteConstants.TopStories)]
		public ActionResult<IEnumerable<ApiFanFictionStoryOutputModel>> TopStories()
		{
			var stories = this.ApiService.TopStories();
			return new List<ApiFanFictionStoryOutputModel>(stories);
		}

		// GET: api/StoriesByGenre
		[HttpGet]
		[Route(GlobalConstants.RouteConstants.StoriesByGenre)]
		public ActionResult<IEnumerable<ApiFanFictionStoryOutputModel>> StoriesByGenre(string genre)
		{
			if (genre == null)
			{
				return NotFound();
			}
			var stories = this.ApiService.StoriesByGenre(genre);
			return new List<ApiFanFictionStoryOutputModel>(stories);
		}
	}
}