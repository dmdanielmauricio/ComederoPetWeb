using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComederoPetWeb.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(user))
            {
                Response.Redirect("/Auth/Login");
            }

            // si está logueado, sigue normalmente
            return Page();
        }

    }
}
