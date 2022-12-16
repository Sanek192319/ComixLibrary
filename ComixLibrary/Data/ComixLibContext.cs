using Microsoft.EntityFrameworkCore;

namespace Data;

public class ComixLibContext : DbContext 
{
    public ComixLibContext(DbContextOptions<ComixLibContext> options) : base(options) { }

    public DbSet<Comix> Comixes { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TrackChanges();
        return await base.SaveChangesAsync();
    }

    private void TrackChanges()
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
    }
}
 