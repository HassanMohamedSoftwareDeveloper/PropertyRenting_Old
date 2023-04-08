using ErrorOr;
using PropertyRenting.Domain.Primitives;
using System.Text.RegularExpressions;

namespace PropertyRenting.Domain.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    private EmailAddress(string value) => Value = value;

    public string Value { get; }

    public static ErrorOr<EmailAddress> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.Errors.EmailAddress.Empty;
        if (Regex.IsMatch(email, "[a-z0-9]+@[a-z]+\\.[a-z]{2,3}'") is false)
            return Errors.Errors.EmailAddress.Invalid;
        return new EmailAddress(email);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
