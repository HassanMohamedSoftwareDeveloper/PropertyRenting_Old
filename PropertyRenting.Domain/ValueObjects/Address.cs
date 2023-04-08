using ErrorOr;

namespace PropertyRenting.Domain.ValueObjects
{
    public sealed record Address
    {
        private Address(int countryId,
                        int cityId,
                        string? areaCode,
                        string? postalCode)
        {
            CountryId = countryId;
            CityId = cityId;
            AreaCode = areaCode;
            PostalCode = postalCode;
        }

        public int CountryId { get; private init; }
        public int CityId { get; private init; }
        public string? AreaCode { get; private init; }
        public string? PostalCode { get; private init; }

        public static ErrorOr<Address> Create(int countryId,
                                              int cityId,
                                              string? areaCode,
                                              string? postalCode)
        {
            return new Address(countryId, cityId, areaCode, postalCode);
        }
    }
}