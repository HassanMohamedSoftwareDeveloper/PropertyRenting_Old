using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class NationalityProfile : Profile
{
    public NationalityProfile()
    {
        CreateMap<NationalityDTO, NationalityEntity>().ReverseMap();
        CreateMap<NationalityEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }

}
