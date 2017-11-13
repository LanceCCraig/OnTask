using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides the main controller for the application.
    /// </summary>
    public class HomeController : Controller
    {
        #region Public Interface
        /// <summary>
        /// Gets the view when an error is encountered.
        /// </summary>
        /// <returns>The view when an error is encountered.</returns>
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        /// <summary>
        /// Gets the default view.
        /// </summary>
        /// <returns>The default view.</returns>
        public IActionResult Index()
        {
            return View();
        } 
        #endregion
    }
}
