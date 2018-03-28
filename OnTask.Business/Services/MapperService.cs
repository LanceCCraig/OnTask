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
            AddEventMapToItself();
            AddEventToEventFullMap();
            AddEventTypeMap();
            AddEventGroupMap();
            AddEventParentMap();
            AddRecurringEventToEventMap();
        }
        #endregion

        #region Private Helpers
        private void AddEventMap() => AddMap<Event, EventModel>(entity =>
        {
            return (EventModel)new EventModel
            {
                EventGroupName = entity.EventGroup?.Name,
                EventGroupWeight = entity.EventGroup?.Weight,
                EventParentName = entity.EventParent?.Name,
                EventParentWeight = entity.EventParent?.Weight,
                EventTypeName = entity.EventType?.Name,
                EventTypeWeight = entity.EventType?.Weight,
                IsEventTypeRecommended = entity.EventType?.IsRecommended ?? true,
            }.InjectFrom<SmartInjection>(entity);
        });

        private void AddEventMapToItself() => AddMap<EventModel, EventModel>(model =>
        {
            return (EventModel)new EventModel().InjectFrom<SmartInjection>(model);
        });

        private void AddEventToEventFullMap() => AddMap<Event, EventFullModel>(entity =>
        {
            return (EventFullModel)new EventFullModel
            {
                EventGroup = entity.EventGroup != null ? Map<EventGroupModel>(entity.EventGroup) : null,
                EventParent = entity.EventParent != null ? Map<EventParentModel>(entity.EventParent) : null,
                EventType = entity.EventType != null ? Map<EventTypeModel>(entity.EventType) : null,
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

        private void AddRecurringEventToEventMap() => AddMap<RecurringEventModel, EventModel>(recurringEventModel =>
        {
            return (EventModel)new EventModel().InjectFrom<SmartInjection>(recurringEventModel);
        });
        #endregion
    }
}
