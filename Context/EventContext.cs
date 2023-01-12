using Microsoft.EntityFrameworkCore;
using eventful_api_master.Models;
using eventful_api_master.DataConfig;

namespace eventful_api_master.Context
{
    public partial class EventContext: DbContext
    {
        public EventContext() {}

        public EventContext(DbContextOptions<EventContext> options): base(options) {}

        public virtual DbSet<Event>? Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(new EventConfiguration().Configure);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
