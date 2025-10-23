using Microsoft.EntityFrameworkCore;
using ApiComederoPet.Data;
using ApiComederoPet.Models;



namespace ComederoPetWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
