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
            public const string Lookup = "account-lookup";
        }
        public static class Building
        {
            public const string Lookup = "building-lookup";
        }
        public static class CashBank
        {
            public const string Lookup = "cash-bank-lookup";
        }
        public static class City
        {
            public const string Lookup = "city-lookup";
            public const string LookupByBuilding = "city-lookup-{0}";
        }
        public static class ContractAdditions
        {
            public const string Lookup = "contract-additions-lookup";
        }
        public static class Contributor
        {
            public const string Lookup = "contributor-lookup";
        }
        public static class Country
        {
            public const string Lookup = "country-lookup";
        }
        public static class District
        {
            public const string Lookup = "district-lookup";
            public const string LookupByCity = "district-lookup-{0}";
        }
        public static class Employee
        {
            public const string Lookup = "employee-lookup";
        }
        public static class Expense
        {
            public const string Lookup = "expense-lookup";
        }
        public static class Nationality
        {
            public const string Lookup = "nationality-lookup";
        }
        public static class Owner
        {
            public const string Lookup = "owner-lookup";
        }
        public static class Renter
        {
            public const string Lookup = "renter-lookup";
        }
        public static class Unit
        {
            public const string Lookup = "unit-lookup";
        }
    }
}
