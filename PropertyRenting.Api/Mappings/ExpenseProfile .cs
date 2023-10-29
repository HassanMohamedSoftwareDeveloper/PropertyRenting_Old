using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<ExpenseDTO, ExpenseEntity>().ReverseMap();
        CreateMap<ExpenseEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}