using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace ComederoPetWeb.Pages
{
    public class HorariosModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiUrl = "https://apicomederopet.onrender.com/api/schedule";

        public HorariosModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<ScheduleItem>? Schedules { get; set; }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Schedules = JsonSerializer.Deserialize<List<ScheduleItem>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                Schedules = new List<ScheduleItem>();
            }
        }

        public async Task<IActionResult> OnPostAddAsync(string Time, string Days)
        {
            var client = _httpClientFactory.CreateClient();

            var newSchedule = new
            {
                userId = 1, // puedes ajustar según login
                time = DateTime.Parse(Time),
                days = Days,
                active = true
            };

            var content = new StringContent(
                JsonSerializer.Serialize(newSchedule),
                Encoding.UTF8,
                "application/json");

            await client.PostAsync(apiUrl, content);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"{apiUrl}/{id}");
            return RedirectToPage();
        }

        public class ScheduleItem
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public DateTime Time { get; set; }
            public string Days { get; set; } = string.Empty;
            public bool Active { get; set; }
        }
    }
}
