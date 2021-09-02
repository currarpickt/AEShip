using Microsoft.EntityFrameworkCore;

namespace AEShip.Service.Models
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options): base(options){}

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Port> Ports { get; set; }
    }
}
