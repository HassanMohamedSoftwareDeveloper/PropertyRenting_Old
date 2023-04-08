namespace PropertyRenting.Api.Enums;

public static class EnumHelper
{
    public static string ToEnumString<T>(this T source) where T : struct
        => Enum.GetName(typeof(T), source);
}
