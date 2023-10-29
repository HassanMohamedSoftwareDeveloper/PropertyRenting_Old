using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class ContractAdditionsProfile : Profile
{
    public ContractAdditionsProfile()
    {
        CreateMap<ContractAdditionsDTO, ContractAdditionsEntity>().ReverseMap();
        CreateMap<ContractAdditionsEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
