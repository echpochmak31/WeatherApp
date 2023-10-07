using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lombok.NET;

namespace WeatherApp.Models.Security;

[Table("user")]
public class User
{
    [Key, Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("email"), Required]
    public string Email { get; set; }

    [Column("password"), Required]
    public string Password { get; set; }

    public List<LocationGroup> LocationGroups { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}