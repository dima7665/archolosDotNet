using archolosDotNet.Models;
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
    }
}