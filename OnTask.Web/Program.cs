using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OnTask.Web
{
    /// <summary>
    /// Provides functionality to build and start the application.
    /// </summary>
    public class Program
    {
        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The provided arguments.</param>
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();
        #endregion

        #region Public Interface
        /// <summary>
        /// Builds the configured web host.
        /// </summary>
        /// <param name="args">The provided arguments.</param>
        /// <returns>The newly built web host.</returns>
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build(); 
        #endregion
    }
}
