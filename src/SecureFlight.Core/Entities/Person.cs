namespace SecureFlight.Core.Entities;

public abstract class Person
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
        
    public string Email { get; set; }
}