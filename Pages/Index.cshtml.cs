using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirige automáticamente al dashboard
            return RedirectToPage("/Dashboard");
        }
    }
}
