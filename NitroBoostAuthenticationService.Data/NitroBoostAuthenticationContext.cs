using Microsoft.EntityFrameworkCore;
using NitroBoostAuthenticationService.Data.Entities;

namespace NitroBoostAuthenticationService.Data;

public class NitroBoostAuthenticationContext : DbContext
{
    public DbSet<Salt> Salts { get; set; }
    
    public NitroBoostAuthenticationContext()
    {
    }

    public NitroBoostAuthenticationContext(DbContextOptions<NitroBoostAuthenticationContext> options)
        : base(options)
    {
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     //optionsBuilder.UseNpgsql("Host=35.234.101.141;Port=5432;Database=authentication-database;Username=postgres;Password=YCR-200400;");
    //     optionsBuilder.UseNpgsql("Host=localhost;Database=authentication-database;Username=blurrito;Password=YCR-200400;");
    // }
}