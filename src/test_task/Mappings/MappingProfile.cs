using AutoMapper;
using TestTask.Data.DTO.Response;

namespace TestTask.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<IEnumerable<TestTask.Data.Route>, SearchResponse>()
                 
               .ForMember(dest => dest.Routes, opt => opt.MapFrom(src => src))
               .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.Min(o => o.Price)))
               .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.Max(o => o.Price)))
               .ForMember(dest => dest.MinMinutesRoute, opt => opt.MapFrom(src => src.Select(o => o.DestinationDateTime - o.OriginDateTime).Min(o => o.TotalMinutes)))
               .ForMember(dest => dest.MaxMinutesRoute, opt => opt.MapFrom(src => src.Select(o => o.DestinationDateTime - o.OriginDateTime).Max(o => o.TotalMinutes)))
               
               .ForAllMembers(opts => opts.PreCondition(src => src?.Any() ?? false ));
        }
    }
}
