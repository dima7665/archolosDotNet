using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        // Tables in db
        public DbSet<BaseItem> Items { get; set; } = default!;
        public DbSet<ConsumableStat> ConsumableStats { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BaseItem>().HasMany(e => e.consumableStats).WithOne().HasForeignKey(e => e.consumableId).IsRequired();

            modelBuilder.Entity<ConsumableStat>().HasOne<BaseItem>().WithMany(e => e.consumableStats).HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasIndex(e => new { e.consumableId, e.stat, e.isPermanent }).IsUnique();
        }
    }
}