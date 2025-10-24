using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            // ?? Evita que la página quede en caché (esto impide volver con "Atrás")
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // ?? Verifica si el usuario está en sesión
            var user = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(user))
            {
                // Redirige si no hay sesión activa
                return RedirectToPage("/Auth/Login");
            }

            // Si hay sesión válida, continúa al Dashboard
            return Page();
        }
    }
}
