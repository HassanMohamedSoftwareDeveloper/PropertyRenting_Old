using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<ExpenseDTO, ExpenseEntity>().ReverseMap();
    }
}