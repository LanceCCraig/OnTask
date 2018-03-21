using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static OnTask.Common.Extensions;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides recommendations for ideal event completion.
    /// </summary>
    public class RecommendationService : BaseService, IRecommendationService
    {
        #region Fields
        private IEventService eventService;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationService"/> class.
        /// </summary>
        /// <param name="eventService">The service that interacts with <see cref="EventModel"/> classes.</param>
        public RecommendationService(IEventService eventService)
        {
            this.eventService = eventService;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Sets the <see cref="BaseService.ApplicationUser"/> for the <see cref="RecommendationService"/> and child services.
        /// </summary>
        /// <param name="applicationUser">The current <see cref="User"/> for the application.</param>
        public override void AddApplicationUser(User applicationUser)
        {
            base.AddApplicationUser(applicationUser);
            eventService.AddApplicationUser(applicationUser);
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets all of the recommended starting point for relevant <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="end">The ending <see cref="DateTime"/> for the time period to recommend.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="RecommendationModel"/> classes.</returns>
        public IEnumerable<RecommendationModel> GetRecommendations(DateTime end)
        {
            try
            {
                var start = DateTime.Now;
                var timePeriod = GetDateRange(start, end).ToList();
                var eventsToBeRecommended = eventService
                    .GetAll(new EventGetAllModel
                    {
                        DateRangeStart = start,
                        DateRangeEnd = end
                    })
                    .Where(x => x.IsEventTypeRecommended)
                    .OrderBy(x => CalculatePriority(x, start))
                    .ToList();

                var timePeriodIndex = 0;
                var maxTimePeriodIndex = timePeriod.Count - 1;
                var recommendations = new List<RecommendationModel>();
                foreach (var eventToBeRecommended in eventsToBeRecommended)
                {
                    recommendations.Add(new RecommendationModel
                    {
                        Event = eventToBeRecommended,
                        RecommendedStartDate = timePeriod[timePeriodIndex]
                    });
                    timePeriodIndex++;
                    if (timePeriodIndex == maxTimePeriodIndex)
                    {
                        timePeriodIndex = 0;
                    }
                }
                return recommendations;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Private Helpers
        private static int CalculatePriority(EventModel model, DateTime start)
        {
            var parentWeight = model.EventParentWeight ?? 1;
            var groupWeight = model.EventGroupWeight ?? 1;
            var typeWeight = model.EventTypeWeight ?? 1;
            var eventWeight = model.Weight ?? 1;
            var minutesUntil = model.StartDate.Subtract(start).Minutes;
            return
                parentWeight *
                groupWeight *
                typeWeight *
                eventWeight *
                minutesUntil;
        }
        #endregion
    }
}
