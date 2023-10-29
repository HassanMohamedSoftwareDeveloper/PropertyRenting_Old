namespace PropertyRenting.Api.Mappings;

public class CashBankProfile : Profile
{
    public CashBankProfile()
    {
        CreateMap<CashBankDTO, CashBankEntity>().ReverseMap();
    }
}
