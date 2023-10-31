namespace PropertyRenting.Api.Constants;

public static class Constants
{
    public static class Language
    {
        public const string LanguageHeaderKey = "x-lang";
        public const string ArabicLanguageCode = "ar";
        public const string EnglishLanguageCode = "en";
    }
    public static class CacheKeys
    {
        public static class Account
        {
            public const string Prefix = "account";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Building
        {
            public const string Prefix = "building";
            public const string Lookup = $"{Prefix}-lookup";
            public const string CountByConstructionStatus = $"{Prefix}-CountByConstructionStatus";
            public const string CountByBuildingType = $"{Prefix}-CountByBuildingType";
            public const string CountByCity = $"{Prefix}-CountByCity";
            public const string RentedUnitsPercentage = $"{Prefix}-RentedUnitsPercentage";
        }
        public static class CashBank
        {
            public const string Prefix = "cash-bank";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class City
        {
            public const string Prefix = "city";
            public const string Lookup = $"{Prefix}-lookup";
            public const string LookupByBuilding = $"{Prefix}-lookup-{{0}}";
        }
        public static class ContractAdditions
        {
            public const string Prefix = "contract-additions";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Contributor
        {
            public const string Prefix = "contributor";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Country
        {
            public const string Prefix = "country";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class District
        {
            public const string Prefix = "district";
            public const string Lookup = $"{Prefix}-lookup";
            public const string LookupByCity = $"{Prefix}-lookup-{{0}}";
        }
        public static class Employee
        {
            public const string Prefix = "employee";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Expense
        {
            public const string Prefix = "expense";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Nationality
        {
            public const string Prefix = "nationality";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Owner
        {
            public const string Prefix = "owner";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Renter
        {
            public const string Prefix = "renter";
            public const string Lookup = $"{Prefix}-lookup";
        }
        public static class Unit
        {
            public const string Prefix = "unit";
            public const string Lookup = $"{Prefix}-lookup";
            public const string CountByCity = $"{Prefix}-CountByCity";
            public const string CountByDistrict = $"{Prefix}-CountByDistrict";
        }
        public static class RenterContract
        {
            public const string Prefix = "renter-contract";
            public const string CountByState = $"{Prefix}-CountByState";
        }
    }
}
