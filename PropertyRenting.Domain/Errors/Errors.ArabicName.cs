using ErrorOr;

namespace PropertyRenting.Domain.Errors
{
    public static partial class Errors
    {
        public static class ArabicName
        {
            public static Error Empty => Error.Validation(code: "AR-Name.Empty", description: "Arabic name can't be empty.");
        }
    }
}
