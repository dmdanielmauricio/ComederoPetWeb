using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpia todos los datos de sesión
            HttpContext.Session.Clear();

            // Redirige al login
            return RedirectToPage("/Login");
        }
    }
}
