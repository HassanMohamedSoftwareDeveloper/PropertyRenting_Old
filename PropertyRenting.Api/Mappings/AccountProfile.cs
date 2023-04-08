using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDTO, AccountEntity>()
            .ReverseMap()
            .ForMember(dest => dest.AccountChildren, opt => opt.MapFrom(srsrc => srsrc.AccountChildren.OrderBy(x => x.CreatedOnUtc)));
        CreateMap<AccountSetupDTO, AccountSetupEntity>().ReverseMap();
    }
}
