using AutoMapper;
using PersonApp.Model;
using PersonApp.DTO;

namespace PersonApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }

}
