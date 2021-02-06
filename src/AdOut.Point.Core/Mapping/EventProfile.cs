using AdOut.Point.Model.Database;
using AdOut.Point.Model.Events;
using AutoMapper;

namespace AdOut.Point.Core.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<AdPoint, AdPointCreatedEvent>();
            CreateMap<AdPoint, AdPointDeletedEvent>();

            CreateMap<PlanCreatedEvent, Plan>();
            CreateMap<PlanAdPointCreatedEvent, PlanAdPoint>();
        }
    }
}
