using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyRenting.UI.Models;
using PropertyRenting.UI.Resources;

namespace PropertyRenting.UI.Pages
{
    public class IndexModel : PageModel
    {
        public readonly List<BreadCrumbLinkVM> Links = new()
        {
           new BreadCrumbLinkVM{DisplayValue=LayoutRes.HomePage,LinkURL=Path.Combine("/") ,IsActive=true},
        };
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}