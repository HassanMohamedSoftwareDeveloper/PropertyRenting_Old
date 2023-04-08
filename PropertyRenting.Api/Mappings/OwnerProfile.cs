using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        CreateMap<OwnerDTO, OwnerEntity>().ReverseMap();
    }
}
