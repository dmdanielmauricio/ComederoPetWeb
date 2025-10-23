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

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule";  // ✅ usa la base configurada
        Horarios = await client.GetFromJsonAsync<List<ScheduleDto>>(apiUrl) ?? new();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule";

        var schedule = new ScheduleDto
        {
            UserId = 1, // más adelante usarás el usuario real logueado
            Time = DateTime.Today.Add(NewTime),
            Active = true
        };

        await client.PostAsJsonAsync(apiUrl, schedule);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule/{id}";
        await client.DeleteAsync(apiUrl);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToggleAsync(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"{ApiBaseUrl}Schedule";

        var horarios = await client.GetFromJsonAsync<List<ScheduleDto>>(apiUrl);
        var item = horarios?.FirstOrDefault(h => h.Id == id);
        if (item == null) return RedirectToPage();

        item.Active = !item.Active;
        await client.PostAsJsonAsync(apiUrl, item); // se actualizará (usa POST por ahora)

        return RedirectToPage();
    }

    public class ScheduleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public bool Active { get; set; }
    }
}
