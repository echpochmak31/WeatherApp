using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lombok.NET;

namespace WeatherApp.Models.Security;

[Table("users")]
public class User
{
    [Key] [Column("id")] public long Id { get; set; }

    [Column("email")] public string Email { get; set; }
    [Column("password")] public string Password { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}