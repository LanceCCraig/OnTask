using OnTask.Business.Models.Event;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;

namespace OnTask.Business.Builders.Interfaces
{
    /// <summary>
    /// Exposes the ability to create <see cref="RecommendationModel"/> classes for the given <see cref="EventModel"/> classes.
    /// </summary>
    public interface IRecommendationsBuilder
    {
        /// <summary>
        /// Sets the current <see cref="User"/> to the buidler.
        /// </summary>
        /// <param name="applicationUser">The current <see cref="User"/> of the application.</param>
        void AddApplicationUser(User applicationUser);
        /// <summary>
        /// Builds the <see cref="List{T}"/> of <see cref="RecommendationModel"/> classes.
        /// </summary>
        /// <returns>The <see cref="List{T}"/> of <see cref="RecommendationModel"/> classes.</returns>
        List<RecommendationModel> Build();
        /// <summary>
        /// Iteratively constructs the <see cref="RecommendationModel"/> classes by date.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder ConstructIterative();
        /// <summary>
        /// Creates the initial data for the <see cref="IRecommendationsBuilder"/>.
        /// </summary>
        /// <param name="start">The start of the <see cref="DateTime"/> range to recommend.</param>
        /// <param name="end">The end of the <see cref="DateTime"/> range to recommend.</param>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder Create(DateTime start, DateTime end);
        /// <summary>
        /// Fills empty gaps in the time period with later <see cref="RecommendationModel"/> classes.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder FillEmpty();
        /// <summary>
        /// Reorders the <see cref="RecommendationModel"/> classes that are recommended after their start date.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder ReorderOverruns();
        /// <summary>
        /// Spreads out the largest clusters of <see cref="RecommendationModel"/> classes from the end of the time period to the start.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder SpreadBackwards();
        /// <summary>
        /// Spreads out the largest clusters of <see cref="RecommendationModel"/> classes from the start of the time period to the end.
        /// </summary>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder SpreadForwards();
        /// <summary>
        /// Sets the initial data for the <see cref="IRecommendationsBuilder"/>.
        /// </summary>
        /// <param name="eventsToRecommend">The <see cref="EventModel"/> classes to recommend.</param>
        /// <param name="timePeriod">The <see cref="DateTime"/> values spanning from the start of the time period to the end.</param>
        /// <returns>The current <see cref="IRecommendationsBuilder"/> instance.</returns>
        IRecommendationsBuilder WithInitial(List<EventModel> eventsToRecommend, List<DateTime> timePeriod);
    }
}
