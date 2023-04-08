using ErrorOr;
using PropertyRenting.Domain.Primitives;

namespace PropertyRenting.Domain.ValueObjects;

public sealed class ArabicName : ValueObject
{
    private ArabicName(string value) => Value = value;

    public string Value { get; }

    public static ErrorOr<ArabicName> Create(string arabicName)
    {
        if (string.IsNullOrWhiteSpace(arabicName))
            return Errors.Errors.ArabicName.Empty;

        return new ArabicName(arabicName);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
