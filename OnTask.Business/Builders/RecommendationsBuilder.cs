using OnTask.Business.Builders.Interfaces;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Common;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static OnTask.Common.Extensions;

namespace OnTask.Business.Builders
{
    /// <summary>
    /// Provides the ability to create <see cref="RecommendationModel"/> classes for the given <see cref="EventModel"/> classes.
    /// </summary>
    public class RecommendationsBuilder : IRecommendationsBuilder
    {
        #region Fields
        private User applicationUser;
        private readonly IEventService eventService;

        private List<EventModel> eventsToRecommend;
        private Dictionary<DateTime, List<RecommendationModel>> inProgressRecommendations;
        private List<DateTime> timePeriod;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsBuilder"/>
        /// </summary>
        /// <param name="eventService">The service that interacts with <see cref="EventModel"/> classes.</param>
        public RecommendationsBuilder(IEventService eventService)
        {
            this.eventService = eventService;
        }
        #endregion

        #region Properties
        private Dictionary<DateTime, List<RecommendationModel>> BlankInProgressRecommendations => timePeriod.ToDictionary(
            k => k,
            v => new List<RecommendationModel>());
        private List<DateTime> Gaps
        {
            get
            {
                var gaps = new List<DateTime>();
                var previousDateIsEmpty = false;
                for (int i = 0; i < timePeriod.Count; i++)
                {
                    var date = timePeriod[i];
                    var currentDateIsEmpty = !inProgressRecommendations[date].Any();
                    if (previousDateIsEmpty && !currentDateIsEmpty)
                    {
                        gaps.Add(timePeriod[i - 1]);
                    }
                    previousDateIsEmpty = currentDateIsEmpty;
                }
                return gaps;
            }
        }
        private int IdealClusterCount => Convert.ToInt32(Math.Ceiling(eventsToRecommend.Count / Convert.ToDecimal(timePeriod.Count)));
        private int MaxClusterCount
        {
            get
            {
                var maxClusterCount = 0;
                foreach (var date in timePeriod)
                {
                    var clusterCount = inProgressRecommendations[date].Count;
                    if (clusterCount > maxClusterCount)
                    {
                        maxClusterCount = clusterCount;
                    }
                }
                return maxClusterCount;
            }
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Sets the current <see cref="User"/> to the buidler.
        /// </summary>
        /// <param name="applicationUser">The current <see cref="User"/> of the application.</param>
        public void AddApplicationUser(User applicationUser)
        {
            this.applicationUser = applicationUser;
            eventService.AddApplicationUser(applicationUser);
        }

        /// <summary>
        /// Builds the <see cref="List{T}"/> of <see cref="RecommendationModel"/> classes.
        /// </summary>
        /// <returns>The <see cref="List{T}"/> of <see cref="RecommendationModel"/> classes.</returns>
        public List<RecommendationModel> Build()
        {
            var recommendations = new List<RecommendationModel>();

            foreach (var date in timePeriod)
            {
                recommendations.AddRange(inProgressRecommendations[date].OrderBy(x => x.Priority));
            }
            eventsToRecommend = null;
            inProgressRecommendations = null;
            timePeriod = null;

            return recommendations;
        }

        /// <summary>
        /// Iteratively constructs the <see cref="RecommendationModel"/> classes by date.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder ConstructIterative() => ExecuteAction(nameof(ConstructIterative), () =>
        {
            var start = timePeriod.First();
            var maxTimePeriodIndex = timePeriod.Count - 1;
            var recommendationId = 1;
            var timePeriodIndex = 0;
            foreach (var eventToBeRecommended in eventsToRecommend)
            {
                inProgressRecommendations[timePeriod[timePeriodIndex]].Add(new RecommendationModel
                {
                    Id = recommendationId,
                    Event = eventToBeRecommended,
                    Priority = CalculatePriority(eventToBeRecommended, start),
                    RecommendedStartDate = timePeriod[timePeriodIndex]
                });

                recommendationId++;
                if (timePeriodIndex != maxTimePeriodIndex)
                {
                    timePeriodIndex++;
                }
            }
        });

        /// <summary>
        /// Creates the initial data for the <see cref="IRecommendationsBuilder"/>.
        /// </summary>
        /// <param name="start">The start of the <see cref="DateTime"/> range to recommend.</param>
        /// <param name="end">The end of the <see cref="DateTime"/> range to recommend.</param>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder Create(DateTime start, DateTime end)
        {
            timePeriod = GetDateRange(start, end).ToList();
            eventsToRecommend = eventService
                .GetAll(new EventGetAllModel
                {
                    DateRangeStart = start,
                    DateRangeEnd = end
                })
                .Where(x => x.IsEventTypeRecommended)
                .OrderBy(x => CalculatePriority(x, start))
                .ToList();
            inProgressRecommendations = BlankInProgressRecommendations;
            return this;
        }

        /// <summary>
        /// Fills empty gaps in the time period with later <see cref="RecommendationModel"/> classes.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder FillEmpty() => ExecuteAction(nameof(FillEmpty), () =>
        {
            var gaps = Gaps;
            while (gaps.Any())
            {
                foreach (var date in timePeriod)
                {
                    if (gaps.Any())
                    {
                        var gap = gaps.First();
                        if (date > gap && inProgressRecommendations[date].Any())
                        {
                            var recommendation = inProgressRecommendations[date]
                                .OrderBy(x => x.Priority)
                                .First();
                            recommendation.RecommendedStartDate = gap;
                            inProgressRecommendations[gap].Add(recommendation);
                            inProgressRecommendations[date].Remove(recommendation);
                            gaps.Remove(gap);
                        }
                    }
                }
                gaps = Gaps;
            }
        });

        /// <summary>
        /// Reorders the <see cref="RecommendationModel"/> classes that are recommended after their start date.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder ReorderOverruns() => ExecuteAction(nameof(ReorderOverruns), () =>
        {
            for (int i = 0; i < timePeriod.Count; i++)
            {
                var date = timePeriod[i];
                var dateRecommendations = inProgressRecommendations[date].ToList();
                foreach (var dateRecommendation in dateRecommendations)
                {
                    if (dateRecommendation.RecommendedStartDate >= dateRecommendation.Event.StartDate)
                    {
                        // Go back through the available dates to find the soonest possible recommended start date.
                        bool foundNewDate = false;
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (!foundNewDate)
                            {
                                var newDate = timePeriod[j];
                                if (newDate < dateRecommendation.Event.StartDate)
                                {
                                    foundNewDate = true;
                                    dateRecommendation.RecommendedStartDate = newDate;
                                    inProgressRecommendations[newDate].Add(dateRecommendation);
                                    inProgressRecommendations[date].RemoveAll(x => x.Id == dateRecommendation.Id);
                                }
                            }
                        }
                    }
                }
            }
        });

        /// <summary>
        /// Spreads out the largest clusters of <see cref="RecommendationModel"/> classes from the end of the time period to the start.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder SpreadBackwards() => ExecuteAction(nameof(SpreadBackwards), () =>
        {
            var idealClusterCount = IdealClusterCount;
            var maxClusterCount = MaxClusterCount;

            if (maxClusterCount > idealClusterCount)
            {
                for (int i = timePeriod.Count - 1; i > 0; i--)
                {
                    var date = timePeriod[i];
                    var dateRecommendations = inProgressRecommendations[date]
                                .OrderBy(x => x.Priority)
                                .ToList();
                    var clusterCount = dateRecommendations.Count;

                    while (clusterCount > idealClusterCount)
                    {
                        var dateRecommendation = dateRecommendations.Last();
                        dateRecommendations.Remove(dateRecommendation);
                        var newDate = timePeriod[i - 1];
                        dateRecommendation.RecommendedStartDate = newDate;
                        inProgressRecommendations[newDate].Add(dateRecommendation);
                        inProgressRecommendations[date].RemoveAll(x => x.Id == dateRecommendation.Id);
                        clusterCount--;
                    }
                }
            }
        });

        /// <summary>
        /// Spreads out the largest clusters of <see cref="RecommendationModel"/> classes from the start of the time period to the end.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder SpreadForwards() => ExecuteAction(nameof(SpreadForwards), () =>
        {
            var idealClusterCount = IdealClusterCount;
            var maxClusterCount = MaxClusterCount;

            if (maxClusterCount > idealClusterCount)
            {
                for (int i = 0; i < timePeriod.Count - 1; i++)
                {
                    var date = timePeriod[i];
                    var dateRecommendations = inProgressRecommendations[date]
                                .OrderBy(x => x.Priority)
                                .ToList();
                    var clusterCount = dateRecommendations.Count;
                    var dateRecommendationIndex = clusterCount - 1;

                    var hasCheckedAllRecommendations = false;
                    while (!hasCheckedAllRecommendations &&
                        clusterCount > idealClusterCount)
                    {
                        hasCheckedAllRecommendations = dateRecommendationIndex == 0;
                        var dateRecommendation = dateRecommendations[dateRecommendationIndex];
                        var newDate = timePeriod[i + 1];
                        if (newDate < dateRecommendation.Event.StartDate)
                        {
                            dateRecommendations.Remove(dateRecommendation);
                            dateRecommendation.RecommendedStartDate = newDate;
                            inProgressRecommendations[newDate].Add(dateRecommendation);
                            inProgressRecommendations[date].RemoveAll(x => x.Id == dateRecommendation.Id);
                            clusterCount--;
                        }
                        dateRecommendationIndex--;
                    }
                }
            }
        });

        /// <summary>
        /// Sets the initial data for the <see cref="IRecommendationsBuilder"/>.
        /// </summary>
        /// <param name="eventsToRecommend">The <see cref="EventModel"/> classes to recommend.</param>
        /// <param name="timePeriod">The <see cref="DateTime"/> values spanning from the start of the time period to the end.</param>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        public IRecommendationsBuilder WithInitial(List<EventModel> eventsToRecommend, List<DateTime> timePeriod)
        {
            this.eventsToRecommend = eventsToRecommend;
            this.timePeriod = timePeriod;
            inProgressRecommendations = BlankInProgressRecommendations;
            return this;
        }
        #endregion

        #region Private Helpers
        private static int CalculatePriority(EventModel model, DateTime start)
        {
            var parentWeight = model.EventParentWeight ?? Constants.LowestWeight;
            var groupWeight = model.EventGroupWeight ?? Constants.LowestWeight;
            var typeWeight = model.EventTypeWeight ?? Constants.LowestWeight;
            var eventWeight = model.Weight ?? Constants.LowestWeight;
            var minutesUntil = model.StartDate.Subtract(start).Minutes;
            return
                parentWeight *
                groupWeight *
                typeWeight *
                eventWeight *
                minutesUntil;
        }

        private IRecommendationsBuilder ExecuteAction(string actionName, Action action)
        {
            if (inProgressRecommendations == null)
            {
                // The WithInitial method can also be used for initialization, but it should only be used for testing purposes.
                throw new InvalidOperationException($"The {nameof(Create)} action must be called before the ${actionName} action.");
            }
            action();
            return this;
        }
        #endregion
    }
}
