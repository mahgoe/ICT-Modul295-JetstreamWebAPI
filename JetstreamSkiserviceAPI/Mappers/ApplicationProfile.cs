using AutoMapper;
using JetstreamSkiserviceAPI.DTO;
using JetstreamSkiserviceAPI.Models;

namespace JetstreamSkiserviceAPI.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile() 
        {

            CreateMap<Registration, RegistrationDto>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
             .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.PriorityName))
             .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service.ServiceName));
            CreateMap<RegistrationDto, Registration>();

            CreateMap<Priority, PriorityDto>();
            CreateMap<PriorityDto, Priority>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Status, StatusDto>();
            CreateMap<StatusDto, Status>();
        }
    }
}
