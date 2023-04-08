using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class ContributerProfile : Profile
{
    public ContributerProfile()
    {
        CreateMap<ContributerDTO, ContributerEntity>().ReverseMap();
    }
}
