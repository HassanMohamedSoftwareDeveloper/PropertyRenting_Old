﻿using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Mappings;

public class ExchangeVoucherProfile : Profile
{
    public ExchangeVoucherProfile()
    {
        CreateMap<ExchangeVoucherDTO, ExchangeVoucherEntity>()
            .ForMember(dest => dest.CashBank, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Renter, opt => opt.Ignore())
            .ForMember(dest => dest.Contributer, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.CashBank, opt =>
            {
                opt.Condition(src => src.CashBank != null);
                opt.MapFrom(src => src.CashBank.Name);
            })
            .ForMember(dest => dest.Owner, opt =>
            {
                opt.Condition(src => src.Owner != null);
                opt.MapFrom(src => Localizable.IsArabic ? src.Owner.NameAR : src.Owner.NameEN);
            })
            .ForMember(dest => dest.Renter, opt =>
            {
                opt.Condition(src => src.Renter != null);
                opt.MapFrom(src => Localizable.IsArabic ? src.Renter.NameAR : src.Renter.NameEN);
            })
            .ForMember(dest => dest.Contributer, opt =>
            {
                opt.Condition(src => src.Contributer != null);
                opt.MapFrom(src => Localizable.IsArabic ? src.Contributer.NameAR : src.Contributer.NameEN);
            });


        CreateMap<ExchangeVoucherDetailsDTO, ExchangeVoucherDetailsEntity>()
             .ForMember(dest => dest.Expense, opt => opt.Ignore())
             .ForMember(dest => dest.Building, opt => opt.Ignore())
             .ForMember(dest => dest.Unit, opt => opt.Ignore())
             .ForMember(dest => dest.Addition, opt => opt.Ignore())
             .ReverseMap()
             .ForMember(dest => dest.Expense, opt =>
             {
                 opt.Condition(src => src.Expense != null);
                 opt.MapFrom(src => Localizable.IsArabic ? src.Expense.NameAR : src.Expense.NameEN);
             })
             .ForMember(dest => dest.Building, opt =>
            {
                opt.Condition(src => src.Building != null);
                opt.MapFrom(src => src.Building.Name);
            })
             .ForMember(dest => dest.Unit, opt =>
            {
                opt.Condition(src => src.Unit != null);
                opt.MapFrom(src => Localizable.IsArabic ? src.Unit.UnitNumber : src.Unit.UnitName);
            })
             .ForMember(dest => dest.Addition, opt =>
               {
                   opt.Condition(src => src.Addition != null);
                   opt.MapFrom(src => Localizable.IsArabic ? src.Addition.NameAR : src.Addition.NameEN);
               })
             ;
    }
}
