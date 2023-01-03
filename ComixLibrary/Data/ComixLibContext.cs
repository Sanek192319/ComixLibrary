using Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data;

public class ComixLibContext : DbContext 
{
    public DbSet<Comix> Comixes { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var entities = ChangeTracker.Entries().ToList();
        var added = entities.Where(x => x.State == EntityState.Added).Select(x => x.Entity);
        var updated = entities.Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
        var time = DateTime.Now;

        foreach (var item in added.OfType<BaseEntity>())
        {
            item.CreatedDate = time;
        }

        foreach (var item in updated.OfType<BaseEntity>())
        {
            item.ModifiedDate = DateTime.Now;
        }
        return await base.SaveChangesAsync();
    }

    public ComixLibContext(DbContextOptions<ComixLibContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
}
 