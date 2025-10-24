using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Elimina toda la sesión
            HttpContext.Session.Clear();

            // Evita volver con el botón "Atrás"
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // Redirige al login
            return RedirectToPage("/Auth/Login");
        }
    }
}
