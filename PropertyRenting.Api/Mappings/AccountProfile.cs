namespace PropertyRenting.Api.Mappings;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDTO, AccountEntity>()
            .ReverseMap()
            .ForMember(dest => dest.AccountChildren, opt => opt.MapFrom(srsrc => srsrc.AccountChildren.OrderBy(x => x.CreatedOnUtc)));
        CreateMap<AccountSetupDTO, AccountSetupEntity>().ReverseMap();

        CreateMap<AccountEntity, FlatAccountDto>()
            .ForMember(dest => dest.HasChildren, opt => opt.MapFrom(src => src.AccountChildren != null && src.AccountChildren.Any()));
    }
}
