using Omu.ValueInjecter;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Common.Injections;
using OnTask.Data.Entities;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides mappings from entity to model classes.
    /// </summary>
    public class MapperService : MapperInstance, IMapperService
    {
        #region Initialize
        /// <summary>
        /// Initialize a new instance of the <see cref="MapperService"/> class.
        /// </summary>
        public MapperService()
        {
            AddEventMap();
            AddEventTypeMap();
            AddEventGroupMap();
            AddEventParentMap();
        }
        #endregion

        #region Private Helpers
        private void AddEventMap() => AddMap<Event, EventModel>(entity =>
        {
            return (EventModel)new EventModel
            {
                EventGroupName = entity.EventGroup?.Name,
                EventParentName = entity.EventParent?.Name,
                EventTypeName = entity.EventType?.Name
            }.InjectFrom<SmartInjection>(entity);
        });

        private void AddEventTypeMap() => AddMap<EventType, EventTypeModel>(entity =>
        {
            return (EventTypeModel)new EventTypeModel
            {
                EventGroupName = entity.EventGroup?.Name,
                EventParentName = entity.EventParent?.Name
            }.InjectFrom<SmartInjection>(entity);
        });

        private void AddEventGroupMap() => AddMap<EventGroup, EventGroupModel>(entity =>
        {
            return (EventGroupModel)new EventGroupModel
            {
                EventParentName = entity.EventParent?.Name
            }.InjectFrom<SmartInjection>(entity);
        });

        private void AddEventParentMap() => AddMap<EventParent, EventParentModel>(entity =>
        {
            return (EventParentModel)new EventParentModel().InjectFrom<SmartInjection>(entity);
        });
        #endregion
    }
}
