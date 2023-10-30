namespace PropertyRenting.Api.Helpers;

public static class ResourceHelper
{
    public static string GetResourceValue(this System.Resources.ResourceManager resourceManager, string key)
    {
        var value = resourceManager.GetString(key, new System.Globalization.CultureInfo(Localizable.CurrentCultureName));
        return value;

    }
}
