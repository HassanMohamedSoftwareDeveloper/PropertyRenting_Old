using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitDTO, UnitEntity>()
            .ForMember(dest => dest.District, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.NameAR : src.District.NameEN))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.City.NameAR : src.District.City.NameEN))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.City.Country.NameAR : src.District.City.Country.NameEN))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.District.CityId))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.District.City.CountryId));

        CreateMap<UnitEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.UnitNumber));
    }
}
