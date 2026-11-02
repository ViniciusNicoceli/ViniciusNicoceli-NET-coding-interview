using System.ComponentModel.DataAnnotations;

namespace SecureFlight.Core;

public abstract class InnerError
{
    public string Code { get; set; }

    [Required]
    public abstract string ErrorType { get; }
}