using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComederoPetWeb.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private readonly HttpClient _httpClient;

        public RegisterModel(IHttpClientFactory httpClientFactory)
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

            var response = await _httpClient.PostAsync("https://apicomederopet.onrender.com/api/auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "✅ Usuario registrado correctamente.";
                Username = Password = string.Empty;
            }
            else
            {
                ErrorMessage = "❌ Error al registrar usuario: " + await response.Content.ReadAsStringAsync();
            }

            return Page();
        }
    }
}


