using Imtahan.Model;
using Microsoft.EntityFrameworkCore;

namespace Imtahan.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
