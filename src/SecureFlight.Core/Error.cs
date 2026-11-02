using System.ComponentModel.DataAnnotations;

namespace SecureFlight.Core;

public class Error
{
    [Required]
    public ErrorCode Code { get; set; }

    [Required]
    public string Message { get; set; }
}