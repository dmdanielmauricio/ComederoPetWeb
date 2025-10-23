using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComederoPetWeb.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Todos los campos son obligatorios.";
                return Page();
            }

            var userData = new { username = Username, password = Password };
            var content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://apicomederopet.onrender.com/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Guardar usuario en sesión
                HttpContext.Session.SetString("Username", Username);

                // Redirigir al panel principal (ajusta la ruta si es diferente)
                return RedirectToPage("/Dashboard");
            }
            else
            {
                var errorText = await response.Content.ReadAsStringAsync();
                ErrorMessage = $"❌ Error de inicio de sesión: {errorText}";
                return Page();
            }
        }
    }
}


