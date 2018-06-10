using Microsoft.EntityFrameworkCore;

namespace friendManager.Models
{
    public class friendManagerContext : DbContext
    {
        public friendManagerContext(DbContextOptions<friendManagerContext> options)
            : base(options)
        {
        }

        public friendManagerContext() : base() {}

        public DbSet<friendManager.Models.Friend> Friend { get; set; }

        
    }
}
