using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using OnTask.Business.Models.Account;
using OnTask.Business.Models.Account.Jwt;
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
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

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
                .UseSwaggerUI(options =>
                {
                    options.DocExpansion("none");
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnTask API v1");
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
            // The identity must be added before the authentication is configured, otherwise the options will not be used.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            ConfigureIdentity(services);
            ConfigureGeneral(services);
            ConfigureMvc(services);
            ConfigureAuthentication(services);

            ConfigureBusinessServices(services);
            ConfigureCommonServices(services);
            ConfigureDataServices(services);
            ConfigureWebServices(services);
        }
        #endregion

        #region Private Helpers
        private static void ConfigureIdentity(IServiceCollection services) => services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();

        private static void ConfigureGeneral(IServiceCollection services) => services
            .AddOptions()
            .AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Authorization: Bearer {token}'",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                options.SwaggerDoc("v1", new Info
                {
                    Description = "The Web API for the OnTask application",
                    Title = "OnTask API",
                    Version = "v1"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "OnTask.Web.xml"));
            })
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
            ;//.Configure<MvcOptions>(options =>
                //{
                //    options.Filters.Add(new RequireHttpsAttribute());
                //});

        private static void ConfigureMvc(IServiceCollection services) => services
            .AddMvc()
            .AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddFluentValidation();

        private void ConfigureAuthentication(IServiceCollection services) => services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidAudience = Configuration["Jwt:Issuer"],
                    ValidIssuer = Configuration["Jwt:Issuer"]
                };
            });

        private void ConfigureBusinessServices(IServiceCollection services) => services
            // Models
            .Configure<JwtSettings>(Configuration.GetSection("Jwt"))
            // Services
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<IBaseService, BaseService>()
            .AddTransient<IEventService, EventService>()
            .AddTransient<IEventGroupService, EventGroupService>()
            .AddTransient<IEventParentService, EventParentService>()
            .AddTransient<IEventTypeService, EventTypeService>()
            .AddTransient<IJwtHandler, JwtHandler>()
            .AddTransient<IMapperService, MapperService>()
            .AddTransient<IRecommendationService, RecommendationService>()
            // Validators (Account)
            .AddSingleton<IValidator<ExternalLoginModel>, ExternalLoginModelValidator>()
            .AddSingleton<IValidator<ForgotPasswordModel>, ForgotPasswordModelValidator>()
            .AddSingleton<IValidator<LoginModel>, LoginModelValidator>()
            .AddSingleton<IValidator<RegisterModel>, RegisterModelValidator>()
            .AddSingleton<IValidator<ResetPasswordModel>, ResetPasswordModelValidator>()
            // Validators (Event)
            .AddSingleton<IValidator<EventDeleteMultipleModel>, EventDeleteMultipleModelValidator>()
            .AddSingleton<IValidator<EventGroupDeleteMultipleModel>, EventGroupDeleteMultipleModelValidator>()
            .AddSingleton<IValidator<EventGroupModel>, EventGroupModelValidator>()
            .AddSingleton<IValidator<EventModel>, EventModelValidator>()
            .AddSingleton<IValidator<EventParentModel>, EventParentModelValidator>()
            .AddSingleton<IValidator<EventTypeModel>, EventTypeModelValidator>()
            .AddSingleton<IValidator<EventTypeDeleteMultipleModel>, EventTypeDeleteMultipleModelValidator>();

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
