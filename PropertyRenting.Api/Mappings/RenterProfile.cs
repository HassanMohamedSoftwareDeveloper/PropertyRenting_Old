using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;
public class RenterProfile : Profile
{
    public RenterProfile()
    {
        CreateMap<ContactPersonDTO, ContactPersonEntity>().ReverseMap();

        CreateMap<RenterDTO, RenterEntity>()
                   .ForMember(dest => dest.City, opt => opt.Ignore())
                   .ForMember(dest => dest.Nationality, opt => opt.Ignore())
                   .ForMember(dest => dest.ContactPersons, opt => opt.Ignore())
                   .ReverseMap()
                   .ForMember(dest => dest.City, opt => opt.MapFrom(src => string.Join(" - ", src.City.NameAR, src.City.NameEN)))
                   .ForMember(dest => dest.Country, opt => opt.MapFrom(src => string.Join(" - ", src.City.Country.NameAR, src.City.Country.NameEN)))
                   .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.CountryId))
                   .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => string.Join(" - ", src.Nationality.NameAR, src.Nationality.NameEN)))
                   ;
    }
}
