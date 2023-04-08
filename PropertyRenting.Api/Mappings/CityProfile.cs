using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityDTO, CityEntity>()
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => string.Join(" - ", src.Country.NameAR, src.Country.NameEN)));
    }
}
