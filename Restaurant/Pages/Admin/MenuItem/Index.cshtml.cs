using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Utilities;

namespace Restaurant.Pages.Admin.MenuItem
{
    [Authorize(Roles = StaticDetails.ManagerRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}