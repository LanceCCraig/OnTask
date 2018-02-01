using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnTask.Business.Models.Account;
using OnTask.Business.Models.Event;
using OnTask.Business.Services;
using OnTask.Business.Services.Interfaces;
using OnTask.Business.Validators.Account;
using OnTask.Business.Validators.Event;
using OnTask.Common;
using OnTask.Data.Contexts;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace OnTask.Web
{
    /// <summary>
    /// Provides functionality to initialize configuration for the application.
    /// </summary>
    public class Startup
    {
        #region Properties
        /// <summary>
        /// Gets the configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The builder used to configure the application's request pipeline.</param>
        /// <param name="env">The class which provides information about the hosting environment.</param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                    {
                        HotModuleReplacement = true,
                        ReactHotModuleReplacement = true
                    });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app
                .UseAuthentication()
                //.UseRewriter(new RewriteOptions()
                //    .AddRedirectToHttps())
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "OnTask API v1");
                });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        /// <summary>
        /// Configures the applications services.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Info
                    {
                        Title = "OnTask API",
                        Version = "v1"
                    });
                })
                ;//.Configure<MvcOptions>(options =>
                 //{
                 //    options.Filters.Add(new RequireHttpsAttribute());
                 //});
            services
                .AddMvc()
                .AddFluentValidation();

            ConfigureIdentity(services);
            ConfigureBusinessServices(services);
            ConfigureCommonServices(services);
            ConfigureDataServices(services);
            ConfigureWebServices(services);
        }
        #endregion

        #region Private Helpers
        private static void ConfigureIdentity(IServiceCollection services) => services
            .Configure<IdentityOptions>(options =>
            {
                // Lockout
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
                // Password
                options.Password.RequiredLength = Constants.MinimumPasswordLength;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireUppercase = true;
                // User
                options.User.RequireUniqueEmail = true;
            })
            .ConfigureApplicationCookie(options =>
            {
                // TODO: Configure application cookie paths.
                //options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Expiration = TimeSpan.FromDays(7);
                options.Cookie.HttpOnly = true;
                //options.LoginPath = "/Account/Login";
                //options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
            })
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();

        private static void ConfigureBusinessServices(IServiceCollection services) => services
            // Services
            .AddTransient<IBaseService, BaseService>()
            .AddTransient<IEventService, EventService>()
            // Validators (Account)
            .AddTransient<IValidator<ExternalLoginModel>, ExternalLoginModelValidator>()
            .AddTransient<IValidator<ForgotPasswordModel>, ForgotPasswordModelValidator>()
            .AddTransient<IValidator<LoginModel>, LoginModelValidator>()
            .AddTransient<IValidator<RegisterModel>, RegisterModelValidator>()
            .AddTransient<IValidator<ResetPasswordModel>, ResetPasswordModelValidator>()
            // Validators (Event)
            .AddTransient<IValidator<EventGroupModel>, EventGroupModelValidator>()
            .AddTransient<IValidator<EventModel>, EventModelValidator>()
            .AddTransient<IValidator<EventParentModel>, EventParentModelValidator>()
            .AddTransient<IValidator<EventTypeModel>, EventTypeModelValidator>();

        private static void ConfigureCommonServices(IServiceCollection services)
        {
        }

        private void ConfigureDataServices(IServiceCollection services) => services
            .AddDbContext<AccountDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("OnTask")))
            .AddTransient<IAccountDbContext, AccountDbContext>()
            .AddDbContext<OnTaskDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("OnTask")))
            .AddTransient<IOnTaskDbContext, OnTaskDbContext>();

        private static void ConfigureWebServices(IServiceCollection services) => services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        #endregion
    }
}
