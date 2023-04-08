using ErrorOr;

namespace PropertyRenting.Domain.Errors
{
    public static partial class Errors
    {
        public static class EmailAddress
        {
            public static Error Empty => Error.Validation(code: "Email.Empty", description: "Email address can't be empty.");
            public static Error Invalid => Error.Validation(code: "Email.Invalid", description: "Invalid email address.");
        }
    }
}
