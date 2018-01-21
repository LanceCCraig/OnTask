using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace OnTask.Web.Controllers
{
    // TODO: Remove SampleDataController after sample application is replaced.
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDateIndex"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public class WeatherForecast
        {
            /// <summary>
            /// 
            /// </summary>
            public string DateFormatted { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int TemperatureC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Summary { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
