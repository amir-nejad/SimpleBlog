using AutoMapper;

namespace SimpleBlog.Infrastructure.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Post, Domain.Entities.Post>()
                .ReverseMap();
        }
    }
}
