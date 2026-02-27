using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Models.Item.Weapon;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        // Tables in db
        public DbSet<Consumable> Consumables { get; set; } = default!;
        public DbSet<ConsumableStat> ConsumableStats { get; set; } = default!;

        public DbSet<Weapon> Weapons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consumable>().HasMany(e => e.consumableStats).WithOne().HasForeignKey(e => e.consumableId).IsRequired();

            modelBuilder.Entity<ConsumableStat>().HasOne<Consumable>().WithMany(e => e.consumableStats).HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasIndex(e => new { e.consumableId, e.stat, e.isPermanent }).IsUnique();
        }
    }
}