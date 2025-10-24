using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            // ?? Evita que la p�gina quede en cach� (esto impide volver con "Atr�s")
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // ?? Verifica si el usuario est� en sesi�n
            var user = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(user))
            {
                // Redirige si no hay sesi�n activa
                return RedirectToPage("/Auth/Login");
            }

            // Si hay sesi�n v�lida, contin�a al Dashboard
            return Page();
        }
    }
}
