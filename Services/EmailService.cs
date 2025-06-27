using UserPromo.Models;
using static System.Net.WebRequestMethods;

namespace UserPromo.Services;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly string _functionUrl;

    public EmailService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _functionUrl = "http://localhost:7071/api/SendEmailFunction";
    }

    public async Task SendWelcomeEmailAsync(User user)
    {
        var payload = new { type = "welcome", user };
        await _httpClient.PostAsJsonAsync(_functionUrl, payload);
    }

    public async Task SendEditEmailAsync(User user)
    {
        var payload = new { type = "edit", user };
        await _httpClient.PostAsJsonAsync(_functionUrl, payload);
    }
}
