using archolosDotNet.Models.Item.Armor;
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

        public DbSet<Armor> Armors { get; set; } = default!;
        public DbSet<ArmorStatObj> ArmorStats { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consumable>().HasMany(e => e.consumableStats).WithOne().HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasOne<Consumable>().WithMany(e => e.consumableStats).HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasIndex(e => new { e.consumableId, e.stat, e.isPermanent }).IsUnique();

            modelBuilder.Entity<Armor>().HasMany(e => e.stats).WithOne().HasForeignKey(e => e.armorId).IsRequired();
            modelBuilder.Entity<ArmorStatObj>().HasOne<Armor>().WithMany(e => e.stats).HasForeignKey(e => e.armorId).IsRequired();
            
        }
    }
}