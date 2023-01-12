using eventful_api_master.DataConfig;
using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;

namespace eventful_api_master.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Event>? Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(new EventConfiguration().Configure);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserConfiguration().Configure);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
