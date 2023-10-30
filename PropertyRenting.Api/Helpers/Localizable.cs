// Ignore Spelling: app Localizable

namespace PropertyRenting.Api.Helpers;

public static class Localizable
{
    public static string CurrentCultureName => Thread.CurrentThread.CurrentCulture.Name;
    public static bool IsArabic => CurrentCultureName == Constants.Constants.Language.ArabicLanguageCode;
}