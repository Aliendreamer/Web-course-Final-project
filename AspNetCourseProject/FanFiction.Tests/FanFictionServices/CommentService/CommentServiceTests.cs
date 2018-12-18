namespace FanFiction.Tests.FanFictionServices.CommentService
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Base;
    using FluentAssertions;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using NUnit.Framework;
    using Services.Interfaces;
    using Services.Utilities;
    using ViewModels.InputModels;
    using ViewModels.OutputModels.Stories;

    [TestFixture]
    public class CommentServiceTests : BaseServiceFake
    {
        private ICommentService CommentService => (ICommentService)this.Provider.GetService(typeof(ICommentService));

        [Test]
        public void AddComment_Should_Add_Comment()
        {
            //arrange
            var user = new FanFictionUser
            {
                Id = "user",
                UserName = "CommentingUser"
            };

            var userManager = (UserManager<FanFictionUser>)this.Provider.GetService(typeof(UserManager<FanFictionUser>));

            userManager.CreateAsync(user).GetAwaiter();
            this.Context.SaveChanges();

            var comment = new CommentInputModel
            {
                StoryId = 1,
                CommentAuthor = "CommentingUser",
                CommentedOn = DateTime.Now.Date,
                Message = "SomeComment",
            };

            var commentOut = new CommentOutputModel
            {
                Id = 1,
                Author = "CommentingUser",
                CommentedOn = DateTime.Now.Date,
                Message = "SomeComment",
            };

            //act
            this.CommentService.AddComment(comment);

            //assert
            var result = this.Context.Comments.First();

            var mappedResult = Mapper.Map<CommentOutputModel>(result);

            mappedResult.Should().BeEquivalentTo(commentOut);
        }

        [Test]
        public void DeleteComment_Should_Delete_Comment()
        {
            //arrange

            var user = new FanFictionUser
            {
                Id = "user",
                UserName = "CommentingUser"
            };

            var genre = new StoryType
            {
                Id = 1,
                Name = "Fantasy"
            };

            var story = new FanFictionStory
            {
                Title = "One",
                Id = 1,
                CreatedOn = DateTime.Now,
                Summary = null,
                ImageUrl = GlobalConstants.DefaultNoImage,
                Type = genre,
                AuthorId = "user"
            };

            var comment = new Comment
            {
                StoryId = 1,
                FanFictionUser = user,
                FanFictionStory = story,
                CommentedOn = DateTime.Now.Date,
                Message = "SomeComment",
                UserId = user.Id,
                Id = 1
            };

            var userManager = (UserManager<FanFictionUser>)this.Provider.GetService(typeof(UserManager<FanFictionUser>));
            userManager.CreateAsync(user).GetAwaiter();
            this.Context.Comments.Add(comment);
            this.Context.StoryTypes.Add(genre);
            this.Context.FictionStories.Add(story);
            this.Context.SaveChanges();

            //act

            this.CommentService.DeleteComment(1);
            var result = this.Context.Comments.ToList();

            //assert

            result.Should().BeEmpty();
        }

        [Test]
        public void DeleteAllComments_For_User_Should_Delete_All()
        {
            //arrange

            var user = new FanFictionUser
            {
                Id = "user",
                UserName = "CommentingUser"
            };

            var genre = new StoryType
            {
                Id = 1,
                Name = "Fantasy"
            };

            var story = new FanFictionStory
            {
                Title = "One",
                Id = 1,
                CreatedOn = DateTime.Now,
                Summary = null,
                ImageUrl = GlobalConstants.DefaultNoImage,
                Type = genre,
                AuthorId = "user"
            };

            var comments = new[]
            {
                new Comment
                {
                StoryId = 1,
                FanFictionUser = user,
                FanFictionStory = story,
                CommentedOn = DateTime.Now.Date,
                Message = "SomeComment",
                UserId = user.Id,
                Id = 1
                },
                new Comment
                {
                StoryId = 1,
                FanFictionUser = user,
                FanFictionStory = story,
                CommentedOn = DateTime.Now.Date,
                Message = "SomeCommentTwo",
                UserId = user.Id,
                Id = 2
                }
        };

            var userManager = (UserManager<FanFictionUser>)this.Provider.GetService(typeof(UserManager<FanFictionUser>));
            userManager.CreateAsync(user).GetAwaiter();
            this.Context.Comments.AddRange(comments);
            this.Context.StoryTypes.Add(genre);
            this.Context.FictionStories.Add(story);
            this.Context.SaveChanges();

            //act

            string username = user.UserName;
            this.CommentService.DeleteAllComments(username);

            //assert
            var result = this.Context.Comments.ToList();
            result.Should().BeEmpty();
        }
    }
}