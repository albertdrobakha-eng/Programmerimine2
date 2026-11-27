using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<AssetClass> AssetClasses { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<MonthlyValue> MonthlyValues { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Portfolio>().HasOne(x => x.Owner).WithMany(x => x.Portfolios).HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}
