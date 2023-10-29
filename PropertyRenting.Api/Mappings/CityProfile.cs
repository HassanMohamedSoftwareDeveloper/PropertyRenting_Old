using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityDTO, CityEntity>()
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Country.NameAR : src.Country.NameEN));
    }
}
