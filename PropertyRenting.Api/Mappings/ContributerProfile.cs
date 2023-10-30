using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class ContributerProfile : Profile
{
    public ContributerProfile()
    {
        CreateMap<ContributorDTO, ContributerEntity>().ReverseMap();
        CreateMap<ContributerEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
