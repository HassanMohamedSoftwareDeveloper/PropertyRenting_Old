using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class DistrictProfile : Profile
{
    public DistrictProfile()
    {
        CreateMap<DistrictDTO, DistrictEntity>()
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => Localizable.IsArabic ? src.City.NameAR : src.City.NameEN))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.CountryId))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => Localizable.IsArabic ? src.City.Country.NameAR : src.City.Country.NameEN));
    }

}
