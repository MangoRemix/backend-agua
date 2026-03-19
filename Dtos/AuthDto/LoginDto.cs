using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Auth;

public class LoginDto
{
    [Required]
    public string Cedula { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
