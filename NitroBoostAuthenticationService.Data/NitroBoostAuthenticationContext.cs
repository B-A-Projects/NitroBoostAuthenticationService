using Microsoft.EntityFrameworkCore;
using NitroBoostAuthenticationService.Data.Entities;

namespace NitroBoostAuthenticationService.Data;

public class NitroBoostAuthenticationContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    public NitroBoostAuthenticationContext()
    {
    }

    public NitroBoostAuthenticationContext(DbContextOptions<NitroBoostAuthenticationContext> options)
        : base(options)
    {
    }
}