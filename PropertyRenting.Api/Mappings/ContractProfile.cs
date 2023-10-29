using AutoMapper;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Models.Entities;

namespace PropertyRenting.Api.Mappings;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<OwnerContractDTO, OwnerContractEntity>()
           .ForMember(dest => dest.Building, opt => opt.Ignore())
           .ForMember(dest => dest.Owner, opt => opt.Ignore())
           .ReverseMap()
           .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building.Name))
           .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Owner.NameAR : src.Owner.NameEN));


        CreateMap<RenterContractDTO, RenterContractEntity>()
          .ForMember(dest => dest.Unit, opt => opt.Ignore())
          .ForMember(dest => dest.Renter, opt => opt.Ignore())
          .ReverseMap()
          .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.UnitName))
          .ForMember(dest => dest.UnitNumber, opt => opt.MapFrom(src => src.Unit.UnitNumber))
          .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.Unit.Building.Id))
          .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Unit.Building.Name))
          .ForMember(dest => dest.Renter, opt => opt.MapFrom(src => Localizable.IsArabic ? src.Renter.NameAR : src.Renter.NameEN));

        CreateMap<ContractFinancialTransactionDTO, OwnerFinancialTransactionEntity>()
            .ReverseMap()
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.OwnerContract.BuildingId))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(src => src.OwnerContract.ContractNumber));

        CreateMap<ContractFinancialTransactionDTO, RenterFinancialTransactionEntity>()
            .ForMember(dest => dest.ContractAddition, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.RenterContract.Unit.BuildingId))
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.RenterContract.UnitId))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(src => src.RenterContract.ContractNumber))
            .ForMember(dest => dest.ContractAddition, opt =>
            {
                opt.Condition(x => x.ContractAddition != null);
                opt.MapFrom(src => Localizable.IsArabic ? src.ContractAddition.NameAR : src.ContractAddition.NameEN);
            });
    }
}
