using UserPromo.Models;

namespace UserPromo.Services;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(User user);
    Task SendEditEmailAsync(User user);
}
