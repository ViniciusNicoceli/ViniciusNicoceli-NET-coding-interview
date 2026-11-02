using Microsoft.EntityFrameworkCore;
using SecureFlight.Infrastructure;

namespace SecureFlight.Test;

public class SecureFlightDatabaseTestContext() : SecureFlightDbContext(
    new DbContextOptionsBuilder<SecureFlightDbContext>()
        .UseInMemoryDatabase(nameof(SecureFlightDatabaseTestContext))
        .Options)
{
    public void CreateDatabase()
    {
        Database.EnsureCreated();
    }
    
    public void ResetTracking()
    {
        ChangeTracker.Clear();
    }
    
    public void DisposeDatabase()
    {
        Database.EnsureDeleted();
    }
}