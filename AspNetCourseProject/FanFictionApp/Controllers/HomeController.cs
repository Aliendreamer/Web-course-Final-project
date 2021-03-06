﻿namespace FanFictionApp.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : Controller
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			return this.User.Identity.IsAuthenticated
				? this.View("LoggedHome")
				: this.View();
		}

		[AllowAnonymous]
		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		[AllowAnonymous]
		public IActionResult Privacy()
		{
			return View();
		}

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}