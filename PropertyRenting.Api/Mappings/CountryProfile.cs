using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryDTO, CountryEntity>().ReverseMap();
        CreateMap<CountryEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
