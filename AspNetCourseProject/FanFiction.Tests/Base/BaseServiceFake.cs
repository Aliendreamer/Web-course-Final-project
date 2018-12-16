namespace FanFiction.Tests.Base
{
    using Data;
    using Models;
    using System;
    using Services;
    using AutoMapper;
    using NUnit.Framework;
    using Services.Utilities;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    [TestFixture]
    public class BaseServiceFake
    {
        protected IServiceProvider Provider { get; set; }

        protected FanFictionContext Context { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(x => { x.AddProfile<FanfictionProfile>(); });
            var services = SetServices();
            this.Provider = services.BuildServiceProvider();
            this.Context = this.Provider.GetRequiredService<FanFictionContext>();
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<FanFictionContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid()
                    .ToString()));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddIdentity<FanFictionUser, IdentityRole>()
                .AddEntityFrameworkStores<FanFictionContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            return services;
        }
    }
}