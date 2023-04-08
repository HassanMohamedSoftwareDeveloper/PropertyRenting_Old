using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDTO, EmployeeEntity>().ReverseMap();
    }
}
