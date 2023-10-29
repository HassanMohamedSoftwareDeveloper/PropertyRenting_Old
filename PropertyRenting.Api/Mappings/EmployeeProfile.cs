using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDTO, EmployeeEntity>().ReverseMap();
        CreateMap<EmployeeEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Localizable.IsArabic ? src.NameAR : src.NameEN));
    }
}
