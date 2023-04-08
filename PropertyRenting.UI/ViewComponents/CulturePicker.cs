using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace PropertyRenting.UI.ViewComponents;

public class CulturePicker : ViewComponent
{
    private readonly IOptions<RequestLocalizationOptions> localizationOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CulturePicker(IOptions<RequestLocalizationOptions> localizationOptions, IHttpContextAccessor httpContextAccessor)
    {
        this.localizationOptions = localizationOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke()
    {
        var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
        var _request = _httpContextAccessor.HttpContext.Request;
        var model = new CulturePickerModel
        {
            SupportedCultures = localizationOptions.Value.SupportedUICultures.ToList(),
            CurrentUICulture = cultureFeature.RequestCulture.UICulture,
            BaseUrl = $"{_request.Path}"
        };
        return View(model);
    }
}

public class CulturePickerModel
{
    public string BaseUrl { get; set; }
    public CultureInfo CurrentUICulture { get; set; }
    public List<CultureInfo> SupportedCultures { get; set; }

    public string ToFlagEmoji(string country)
    {
        country = country
            .Split('-')
            .LastOrDefault();

        if (country == null)
            return "⁉️️";

        return string.Concat(
            country
            .ToUpper()
            .Select(x => char.ConvertFromUtf32(x + 0x1F1A5))
        );
    }
}
