namespace FanFictionApp
{
    using AutoMapper;
    using Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using FanFiction.Data;
    using FanFiction.Models;
    using FanFiction.Services;
    using FanFiction.Services.Interfaces;
    using FanFiction.Services.Utilities;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FanFictionContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<FanFictionUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<FanFictionContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<CustomActionFilterAttribute>();
            services.AddScoped<LogExceptionActionFilter>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStoryService, StoryService>();

            services.AddAutoMapper();

            //TODO: Think about this shit? leave it for now though
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = $"/Account/Login";
            //    options.LogoutPath = $"/Account/Logout";
            //    options.AccessDeniedPath = $"/Account/AccessDenied";
            //});

            services.AddMvc(opt =>
            {
                opt.Filters.Add<CustomActionFilterAttribute>();
                opt.Filters.Add<LogExceptionActionFilter>();
                opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSeedRolesMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            Mapper.Initialize(opt =>
            {
                opt.AddProfile<FanfictionProfile>();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}