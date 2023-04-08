﻿using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryDTO, CountryEntity>().ReverseMap();
    }
}
