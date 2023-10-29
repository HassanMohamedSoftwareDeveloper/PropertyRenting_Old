using PropertyRenting.Api.Helpers;

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
                   .ForMember(dest => dest.City, opt => opt.MapFrom(src => Localizable.IsArabic ? src.City.NameAR : src.City.NameEN))
                   .ForMember(dest => dest.Country, opt => opt.MapFrom(src => Localizable.IsArabic ? src.City.Country.NameAR : src.City.Country.NameEN))
                   .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.City.CountryId))
                   .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Nationality.NameAR : src.Nationality.NameEN));

        CreateMap<RenterEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
