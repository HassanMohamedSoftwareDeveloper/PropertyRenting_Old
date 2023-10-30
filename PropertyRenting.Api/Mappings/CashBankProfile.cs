namespace PropertyRenting.Api.Mappings;

public class CashBankProfile : Profile
{
    public CashBankProfile()
    {
        CreateMap<CashBankDTO, CashBankEntity>().ReverseMap();
        CreateMap<CashBankEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));
    }
}
