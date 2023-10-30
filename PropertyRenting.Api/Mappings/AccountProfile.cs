using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDTO, AccountEntity>()
            .ReverseMap()
            .ForMember(dest => dest.AccountChildren, opt => opt.MapFrom(src => src.AccountChildren.OrderBy(x => x.CreatedOnUtc)));
        CreateMap<AccountSetupDTO, AccountSetupEntity>().ReverseMap();

        CreateMap<AccountEntity, FlatAccountDto>()
            .ForMember(dest => dest.HasChildren, opt => opt.MapFrom(src => src.AccountChildren != null && src.AccountChildren.Any()));

        CreateMap<AccountEntity, AccountLookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
