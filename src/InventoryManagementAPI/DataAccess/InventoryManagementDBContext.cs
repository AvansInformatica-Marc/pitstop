namespace Pitstop.InventoryManagementAPI.DataAccess;

using Pitstop.InventoryManagementAPI.Model;

public class InventoryManagementDBContext : DbContext {
    public DbSet<Product> Products { get; set; }

    public InventoryManagementDBContext(DbContextOptions<InventoryManagementDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Product>().HasKey(m => m.ProductId);
        builder.Entity<Product>().ToTable("Product");
        base.OnModelCreating(builder);
    }

    public void MigrateDB() {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}
