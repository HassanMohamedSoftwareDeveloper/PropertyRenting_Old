using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class DistrictProfile : Profile
{
    public DistrictProfile()
    {
        CreateMap<DistrictDTO, DistrictEntity>()
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => string.Join(" - ", src.City.NameAR, src.City.NameEN)))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.CountryId))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => string.Join(" - ", src.City.Country.NameAR, src.City.Country.NameEN)));
    }

}
