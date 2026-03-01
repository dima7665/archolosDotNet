using archolosDotNet.Models.Item.ArmorNS;
using archolosDotNet.Models.Item.ConsumableNS;
using archolosDotNet.Models.Item.Miscellaneous;
using archolosDotNet.Models.Item.RecipeNS;
using archolosDotNet.Models.Item.WeaponNS;
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

        public DbSet<Misc> Miscs { get; set; } = default!;

        public DbSet<Armor> Armors { get; set; } = default!;
        public DbSet<ArmorStatObj> ArmorStats { get; set; } = default!;

        public DbSet<Recipe> Recipes { get; set; } = default!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Consumable
            modelBuilder.Entity<Consumable>().HasMany(e => e.consumableStats).WithOne().HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasOne<Consumable>().WithMany(e => e.consumableStats).HasForeignKey(e => e.consumableId).IsRequired();
            modelBuilder.Entity<ConsumableStat>().HasIndex(e => new { e.consumableId, e.stat, e.isPermanent }).IsUnique();

            // Armor
            modelBuilder.Entity<Armor>().HasMany(e => e.stats).WithOne().HasForeignKey(e => e.armorId).IsRequired();
            modelBuilder.Entity<ArmorStatObj>().HasOne<Armor>().WithMany(e => e.stats).HasForeignKey(e => e.armorId).IsRequired();

            // Recipe
            modelBuilder.Entity<Recipe>().HasMany(e => e.ingredients).WithOne().HasForeignKey(e => e.recipeId).IsRequired();

            modelBuilder.Entity<Recipe>().HasOne(e => e.misc).WithMany(e => e.recipes).HasForeignKey(e => e.id).IsRequired(true);
            modelBuilder.Entity<Misc>().HasMany(e => e.recipes).WithOne(e => e.misc).HasForeignKey(e => e.miscId).IsRequired(false);

            modelBuilder.Entity<Recipe>().HasOne(e => e.consumable).WithMany(e => e.recipes).HasForeignKey(e => e.id).IsRequired(true);
            modelBuilder.Entity<Consumable>().HasMany(e => e.recipes).WithOne(e => e.consumable).HasForeignKey(e => e.consumableId).IsRequired(false);

            modelBuilder.Entity<Recipe>().HasOne(e => e.weapon).WithMany(e => e.recipes).HasForeignKey(e => e.id).IsRequired(true);
            modelBuilder.Entity<Weapon>().HasMany(e => e.recipes).WithOne(e => e.weapon).HasForeignKey(e => e.weaponId).IsRequired(false);

            // Recipe ingredient
            modelBuilder.Entity<RecipeIngredient>().HasOne<Recipe>().WithMany(e => e.ingredients).HasForeignKey(e => e.recipeId).IsRequired();

            modelBuilder.Entity<RecipeIngredient>().HasOne(e => e.misc).WithOne(e => e.asIngredient).HasForeignKey<Misc>(e => e.id).IsRequired(true);
            modelBuilder.Entity<Misc>().HasOne(e => e.asIngredient).WithOne(e => e.misc).HasForeignKey<RecipeIngredient>(e => e.miscId).IsRequired(false);

            modelBuilder.Entity<RecipeIngredient>().HasOne(e => e.consumable).WithOne(e => e.asIngredient).HasForeignKey<Consumable>(e => e.id).IsRequired(true);
            modelBuilder.Entity<Consumable>().HasOne(e => e.asIngredient).WithOne(e => e.consumable).HasForeignKey<RecipeIngredient>(e => e.consumableId).IsRequired(false);

            modelBuilder.Entity<RecipeIngredient>().HasOne(e => e.weapon).WithOne(e => e.asIngredient).HasForeignKey<Weapon>(e => e.id).IsRequired(true);
            modelBuilder.Entity<Weapon>().HasOne(e => e.asIngredient).WithOne(e => e.weapon).HasForeignKey<RecipeIngredient>(e => e.weaponId).IsRequired(false);
        }
    }
}
