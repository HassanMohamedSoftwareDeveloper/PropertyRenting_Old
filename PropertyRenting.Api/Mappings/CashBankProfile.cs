using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class CashBankProfile : Profile
{
    public CashBankProfile()
    {
        CreateMap<CashBankDTO, CashBankEntity>().ReverseMap();
    }
}
