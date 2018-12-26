namespace FanFiction.Tests.FanFictionControllers.StoriesController
{
	using System;
	using Castle.Core.Internal;
	using FanFictionApp.Controllers;
	using FluentAssertions;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Moq;
	using NUnit.Framework;
	using Services.Interfaces;
	using Services.Utilities;
	using ViewModels.InputModels;

	[TestFixture]
	public class StoriesControllerTests
	{
		protected Mock<IStoryService> storyService => new Mock<IStoryService>();

		[Test]
		public void CreateStory_Should_Return_InvalidModel()
		{
			var story = new StoryInputModel
			{
				Author = "some",
				CreatedOn = DateTime.Now,
				Genre = "someGenre",
				StoryImage = null,
				Summary = null,
				Title = null
			};

			var controller = new StoriesController(storyService.Object);
			controller.ModelState.AddModelError("Title", "StringLength");
			var result = controller.CreateStory(story).GetAwaiter().GetResult();

			result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<StoryInputModel>();
		}

		[Test]
		public void CreateStory_Should_Return_InvalidModel_For_Wrong_File_Type()
		{
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(x => x.ContentType).Returns("image/sss");

			var story = new Mock<StoryInputModel>();
			story.Object.StoryImage = fileMock.Object;
			story.SetupAllProperties();

			var controller = new StoriesController(storyService.Object);

			var result = controller.CreateStory(story.Object).GetAwaiter().GetResult();

			result.Should().BeOfType<ViewResult>().Which.ViewData.Values.Should().Contain(GlobalConstants.WrongFileType);
		}

		[Test]
		public void Controller_should_Have_Authorize_Attribute()
		{
			typeof(StoriesController).Should().BeDecoratedWith<AuthorizeAttribute>();
		}
	}
}