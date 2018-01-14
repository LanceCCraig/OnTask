using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnTask.Data.Contexts;
using OnTask.Data.Contexts.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

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
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
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
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services
                .AddEntityFrameworkSqlServer()
                .AddOptions()
                .AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("V1", new Info
                    {
                        Title = "OnTask API",
                        Version = "V1"
                    });
                });

            services.AddMvc(x => x.OutputFormatters.RemoveType<StringOutputFormatter>());

            ConfigureBusinessServices(services);
            ConfigureCommonServices(services);
            ConfigureDataServices(services);
            ConfigureWebServices(services);
        }
        #endregion

        #region Private Helpers
        private static void ConfigureBusinessServices(IServiceCollection services)
        {
        }

        private static void ConfigureCommonServices(IServiceCollection services)
        {
        }

        private void ConfigureDataServices(IServiceCollection services) => services
            .AddDbContext<OnTaskContext>(x => x.UseSqlServer(Configuration.GetConnectionString("OnTask")))
            .AddTransient<IOnTaskContext, OnTaskContext>();

        private static void ConfigureWebServices(IServiceCollection services) => services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        #endregion
    }
}
