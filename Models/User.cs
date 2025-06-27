using System.ComponentModel.DataAnnotations;

namespace UserPromo.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

}
