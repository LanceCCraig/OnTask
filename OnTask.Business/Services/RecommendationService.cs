using OnTask.Business.Builders.Interfaces;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Common;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using static OnTask.Common.Enumerations;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides recommendations for ideal event completion.
    /// </summary>
    public class RecommendationService : BaseService, IRecommendationService
    {
        #region Fields
        private IRecommendationsBuilder builder;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationService"/> class.
        /// </summary>
        /// <param name="builder">The builder for the <see cref="RecommendationModel"/> classes.</param>
        public RecommendationService(IRecommendationsBuilder builder)
        {
            this.builder = builder;
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
            builder.AddApplicationUser(applicationUser);
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets all of the recommended starting point for relevant <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="end">The ending <see cref="DateTime"/> for the time period to recommend.</param>
        /// <param name="mode">Specifies the calculation mode to use.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="RecommendationModel"/> classes.</returns>
        public IEnumerable<RecommendationModel> GetRecommendations(DateTime end, RecommendationMode mode)
        {
            try
            {
                // Handle everything in EST (to make thing easier for now).
                var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.EasternStandardTimeZoneId);
                var start = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, estTimeZone);

                builder
                    .Create(start, end)
                    .ConstructIterative()
                    .ReorderOverruns()
                    .SpreadBackwards();
                if (mode == RecommendationMode.MinimalClustering)
                {
                    builder.SpreadForwards();
                }
                return builder
                    .FillEmpty()
                    .Build();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
