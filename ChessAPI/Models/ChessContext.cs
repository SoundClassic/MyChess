using System.Data.Entity;

namespace ChessAPI.Models
{
    public class ChessContext : DbContext
    {
        public ChessContext() : base("DbConnectionString") { }

        public virtual DbSet<Game> Games { get; set; }
    }
}