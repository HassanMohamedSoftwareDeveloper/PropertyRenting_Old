using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class ContractAdditionsProfile : Profile
{
    public ContractAdditionsProfile()
    {
        CreateMap<ContractAdditionsDTO, ContractAdditionsEntity>().ReverseMap();
    }
}
