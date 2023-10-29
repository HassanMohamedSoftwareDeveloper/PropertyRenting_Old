// Ignore Spelling: app Localizable

namespace PropertyRenting.Api.Helpers;

public static class Localizable
{
    public static bool IsArabic => Thread.CurrentThread.CurrentCulture.Name == Constants.Constants.Language.ArabicLanguageCode;
}