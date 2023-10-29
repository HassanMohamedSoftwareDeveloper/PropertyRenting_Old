using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class BuildingProfile : Profile
{
    public BuildingProfile()
    {
        CreateMap<BuildingContributerDTO, BuildingContributerEntity>()
            .ForMember(dest => dest.Contributer, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Contributer, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Contributer.NameAR : src.Contributer.NameEN))
            ;

        CreateMap<BuildingDTO, BuildingEntity>()
            .ForMember(dest => dest.District, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Contributers, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.NameAR : src.District.NameEN))
            .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Employee.NameAR : src.Employee.NameEN))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.City.NameAR : src.District.City.NameEN))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => Localizable.IsArabic ? src.District.City.Country.NameAR : src.District.City.Country.NameEN))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ((BuildingType)src.TypeId).ToString()))
            .ForMember(dest => dest.ConstructionStatus, opt => opt.MapFrom(src => ((ConstructionStatus)src.ConstructionStatusId).ToString()))
            .ForMember(dest => dest.UnitsNo, opt => opt.MapFrom(src => src.Units.Count))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.District.CityId))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.District.City.CountryId));

        CreateMap<BuildingEntity, LookupDTO>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));
    }
}
