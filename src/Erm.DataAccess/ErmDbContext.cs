using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace Erm.DataAccess;

public sealed class ErmDbContext : DbContext
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=erm_db;Username=postgres;Password=admin;Pooling=true;";

    public DbSet<BusinessProcess> BusinessProcesses { get; set; } = null!;
    public DbSet<RiskProfile> RiskProfiles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}