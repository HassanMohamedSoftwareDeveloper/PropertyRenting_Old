using ErrorOr;
using PropertyRenting.Domain.Primitives;

namespace PropertyRenting.Domain.ValueObjects;

public class EnglishName : ValueObject
{
    private EnglishName(string value) => Value = value;

    public string Value { get; }

    public static ErrorOr<EnglishName> Create(string arabicName)
    {
        if (string.IsNullOrWhiteSpace(arabicName))
            return Errors.Errors.EnglishName.Empty;

        return new EnglishName(arabicName);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
