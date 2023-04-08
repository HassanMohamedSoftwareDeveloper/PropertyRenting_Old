using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class NationalityProfile : Profile
{
    public NationalityProfile()
    {
        CreateMap<NationalityDTO, NationalityEntity>().ReverseMap();
    }

}
