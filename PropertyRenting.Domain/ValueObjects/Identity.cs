using ErrorOr;
using PropertyRenting.Domain.Enums;

namespace PropertyRenting.Domain.ValueObjects;

public sealed record Identity
{
    public IdentityType Type { get; private init; }
    public string? IdentityNumber { get; private init; }
    public string? IssuerPlace { get; private init; }
    public DateOnly IssuerDate { get; private init; }
    public DateOnly ExpiryDate { get; private init; }

    private Identity(IdentityType type,
                     string identityNumber,
                     string issuerPlace,
                     DateOnly issuerDate,
                     DateOnly expiryDate)
    {
        this.Type = type;
        this.IdentityNumber = identityNumber;
        this.IssuerPlace = issuerPlace;
        this.IssuerDate = issuerDate;
        this.ExpiryDate = expiryDate;
    }
    public static ErrorOr<Identity> Create(IdentityType type,
                                           string identityNumber,
                                           string issuerPlace,
                                           DateOnly issuerDate,
                                           DateOnly expiryDate)
    {

        return new Identity(type, identityNumber, issuerPlace, issuerDate, expiryDate);
    }


}
