using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        CreateMap<OwnerDTO, OwnerEntity>().ReverseMap();
        CreateMap<OwnerEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
