using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class HorariosModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public List<ScheduleDto> Horarios { get; set; } = new();

    [BindProperty]
    public TimeSpan NewTime { get; set; }

    public HorariosModel(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    private string ApiBaseUrl => _config["ApiSettings:BaseUrl"];

    // ✅ GET
    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule"; // 🔹 cambio aquí
        Horarios = await client.GetFromJsonAsync<List<ScheduleDto>>(apiUrl) ?? new();
    }

    // ✅ POST - Agregar nuevo horario
    public async Task<IActionResult> OnPostAddAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule"; // 🔹 cambio aquí

        var schedule = new ScheduleDto
        {
            Hour = NewTime.Hours,
            Minute = NewTime.Minutes,
            DaysOfWeek = "Todos",
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        await client.PostAsJsonAsync(apiUrl, schedule);
        return RedirectToPage();
    }

    // ✅ DELETE
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule/{id}"; // 🔹 cambio aquí
        await client.DeleteAsync(apiUrl);
        return RedirectToPage();
    }

    // ✅ TOGGLE
    public async Task<IActionResult> OnPostToggleAsync(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule"; // 🔹 cambio aquí

        var horarios = await client.GetFromJsonAsync<List<ScheduleDto>>(apiUrl);
        var item = horarios?.FirstOrDefault(h => h.Id == id);
        if (item == null) return RedirectToPage();

        item.IsActive = !item.IsActive;
        await client.PutAsJsonAsync($"{apiUrl}/{id}", item);

        return RedirectToPage();
    }

    public class ScheduleDto
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string DaysOfWeek { get; set; } = "";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
