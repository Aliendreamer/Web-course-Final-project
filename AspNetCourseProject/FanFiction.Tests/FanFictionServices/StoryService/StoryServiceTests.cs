namespace FanFiction.Tests.FanFictionServices.StoryService
{
    using Base;
    using Models;
    using System;
    using NUnit.Framework;
    using FluentAssertions;
    using Services.Utilities;
    using Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels.OutputModels.Stories;

    [TestFixture]
    public class StoryServiceTests : BaseServiceFake
    {
        [Test]
        public void CurrentStories_with_Type_Null_Should_Return_All_Stories()
        {
            //arrange
            var stories = new FanFictionStory[]
            {
                new FanFictionStory
                {
                    Title="One",
                    Id=1,
                    CreatedOn=DateTime.Now,
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=1,
                        Name="Fantasy"
                    },
                    AuthorId="1111"
                },
                new FanFictionStory
                {
                    Title="Two",
                    Id=2,
                    CreatedOn=DateTime.Now.AddMonths(1),
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=2,
                        Name="Horror"
                    },
                    AuthorId="2222"
                },
                new FanFictionStory
                {
                    Title="three",
                    Id=3,
                    CreatedOn=DateTime.Now.AddDays(2),
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=3,
                        Name="Science Fiction"
                    },
                    AuthorId="3333"
                },
            };
            this.Context.FictionStories.AddRange(stories);
            this.Context.SaveChanges();

            //act

            var storyServices = this.Provider.GetRequiredService<IStoryService>();
            var result = storyServices.CurrentStories(null);

            //assert

            result.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        [Test]
        public void CurrentStories_with_Type_Null_Should_Return_OnlyOneType_Stories()
        {
            //arrange
            var stories = new FanFictionStory[]
            {
                new FanFictionStory
                {
                    Title="One",
                    Id=1,
                    CreatedOn=DateTime.Now,
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=1,
                        Name="Fantasy"
                    },
                    AuthorId="1111"
                },
                new FanFictionStory
                {
                    Title="Two",
                    Id=2,
                    CreatedOn=DateTime.Now.AddMonths(1),
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=2,
                        Name="Horror"
                    },
                    AuthorId="2222"
                },
                new FanFictionStory
                {
                    Title="three",
                    Id=3,
                    CreatedOn=DateTime.Now.AddDays(2),
                    Summary=null,
                    ImageUrl=GlobalConstants.DefaultNoImage,
                    Type=new StoryType
                    {
                        Id=3,
                        Name="Science Fiction"
                    },
                    AuthorId="3333"
                },
            };

            this.Context.FictionStories.AddRange(stories);
            this.Context.SaveChanges();

            //act
            string storyType = "Fantasy";
            var storyService = this.Provider.GetRequiredService<IStoryService>();
            var result = storyService.CurrentStories(storyType);

            //assert
            result.Should().NotBeEmpty()
                .And.ContainSingle(x => x.Type.Type == "Fantasy");
        }

        [Test]
        public void GenresGetCorrectlyTakenFromDb()
        {
            //arrange
            var genres = new StoryType[]
            {
                new StoryType
                {
                    Id=1,
                    Name = "Fantasy"
                },
                new StoryType
                {
                    Id=2,
                    Name="Young Adult"
                },
                new StoryType
                {
                    Id=3,
                    Name="Comedy"
                }
            };

            this.Context.StoryTypes.AddRange(genres);
            this.Context.SaveChanges();

            //act

            var storyService = this.Provider.GetRequiredService<IStoryService>();

            var genresFromDb = storyService.Genres();

            genresFromDb.Should().HaveCount(3);
        }

        [Test]
        public void GetStoryById_Throw_Exception_with_NonExistant_Id()
        {
            //arrange

            var story = new FanFictionStory
            {
                Title = "One",
                Id = 1,
                CreatedOn = DateTime.Now,
                Summary = null,
                ImageUrl = GlobalConstants.DefaultNoImage,
                Type = new StoryType
                {
                    Id = 1,
                    Name = "Fantasy"
                },
                AuthorId = "1111"
            };

            this.Context.FictionStories.Add(story);
            this.Context.SaveChanges();

            //act
            var storyService = this.Provider.GetRequiredService<IStoryService>();
            Action act = () => storyService.GetStoryById(2);

            act.Should().Throw<ArgumentException>().WithMessage(GlobalConstants.MissingStory);
        }

        [Test]
        public void GetStoryById_Should_Return_StoryDetailsModel_With_Correct_Id()
        {
            //arrange

            var story = new FanFictionStory
            {
                Title = "One",
                Id = 1,
                CreatedOn = DateTime.Now,
                Summary = null,
                ImageUrl = GlobalConstants.DefaultNoImage,
                Type = new StoryType
                {
                    Id = 1,
                    Name = "Fantasy"
                },
                AuthorId = "1111"
            };

            this.Context.FictionStories.Add(story);
            this.Context.SaveChanges();

            //act
            var storyService = this.Provider.GetRequiredService<IStoryService>();

            var result = storyService.GetStoryById(1);

            result.Should().BeOfType<StoryDetailsOutputModel>().And.Should().NotBeNull();
        }

        [Test]
        public void UserStories_Should_Return_The_Stories_for_Unique_Username()
        {
            //arrange

            var stories = new FanFictionStory[]
            {
                new FanFictionStory
                {
                    Id=1,
                    Author=  new FanFictionUser
                    {
                        Nickname = "TestStory",
                        UserName = "WhatEver",
                    },
                    Summary="some summary",
                    Title="Story To test"
                },
                new FanFictionStory
                {
                    Id=2,
                    Author= new FanFictionUser
                    {
                        Nickname = "FalseUser",
                        UserName = "SomeNickname",
                    },
                    Summary=null,
                    Title="another test"
                },
            };

            var storyService = this.Provider.GetRequiredService<IStoryService>();

            //act

            this.Context.FictionStories.AddRange(stories);
            this.Context.SaveChanges();

            string username = "WhatEver";
            var userStories = storyService.UserStories(username);

            //assert
            userStories.Should().HaveCount(1).And.ContainSingle(x => x.Author.Username == username);
        }
    }
}