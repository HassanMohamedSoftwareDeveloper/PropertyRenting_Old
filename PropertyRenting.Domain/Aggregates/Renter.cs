using ErrorOr;
using PropertyRenting.Domain.Enums;
using PropertyRenting.Domain.Primitives;
using PropertyRenting.Domain.ValueObjects;

namespace PropertyRenting.Domain.Aggregates;

public sealed class Renter : AggregateRoot
{
    private Renter(Guid id,
                   bool isActive,
                   ReterTypeEnum type,
                   ArabicName arabicName,
                   EnglishName englishName,
                   Identity identity,
                   Address address) : base(id)
    {
        IsActive = isActive;
        Type = type;
        ArabicName = arabicName;
        EnglishName = englishName;
        Identity = identity;
        Address = address;
    }

    public bool IsActive { get; private set; }
    public ReterTypeEnum Type { get; private set; }
    public ArabicName? ArabicName { get; private set; }
    public EnglishName? EnglishName { get; private set; }
    public Identity Identity { get; private set; }
    public Address Address { get; private set; }
    public string? Email { get; private set; }

    public static ErrorOr<Renter> Create(Guid id,
                                         bool isActive,
                                         ReterTypeEnum type,
                                         ArabicName arabicName,
                                         EnglishName englishName,
                                         Identity identity,
                                         Address address)
    {
        return new Renter(id, isActive, type, arabicName, englishName, identity, address);
    }
}