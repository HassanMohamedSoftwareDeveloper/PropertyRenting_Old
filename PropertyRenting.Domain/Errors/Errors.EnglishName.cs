using ErrorOr;

namespace PropertyRenting.Domain.Errors;

public static partial class Errors
{
    public static class EnglishName
    {
        public static Error Empty => Error.Validation(code: "EN-Name.Empty", description: "English name can't be empty.");
    }
}
