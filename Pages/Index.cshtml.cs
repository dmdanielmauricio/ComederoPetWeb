using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirige autom�ticamente al dashboard
            return RedirectToPage("/Dashboard");
        }
    }
}
