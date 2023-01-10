using Microsoft.EntityFrameworkCore;
using eventful_api_master.Models;
using eventful_api_master.DataConfig;

namespace eventful_api_master.Context
{
    public partial class UserContext: DbContext
    {
        public UserContext() {}

        public UserContext(DbContextOptions<UserContext> options): base(options) {}

        public virtual DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserConfiguration().Configure);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
