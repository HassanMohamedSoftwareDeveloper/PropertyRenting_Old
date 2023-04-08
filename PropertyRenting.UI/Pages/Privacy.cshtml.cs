using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyRenting.UI.Models;
using PropertyRenting.UI.Resources;

namespace PropertyRenting.UI.Pages
{
    public class PrivacyModel : PageModel
    {
        public readonly List<BreadCrumbLinkVM> Links = new()
        {
            new BreadCrumbLinkVM{DisplayValue=LayoutRes.HomePage,LinkURL=Path.Combine("/") ,IsActive=false},
            new BreadCrumbLinkVM{DisplayValue=LayoutRes.PrivacyPage,LinkURL=Path.Combine("/","Privacy") ,IsActive=true},
        };
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}