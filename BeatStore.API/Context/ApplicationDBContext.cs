using BeatStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeatStore.API.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Track>? Tracks { get; set; }
        public DbSet<Stock>? Stock { get; set; }
        public DbSet<OrderDetails>? OrderDetails { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>()
                .HasKey(e => e.Id)
                .HasName("PK_TrackId");
            modelBuilder.Entity<Stock>()
                .HasKey(e => e.Id)
                .HasName("PK_StockId");


            modelBuilder.Entity<OrderDetails>()
                .HasKey(e => e.Id)
                .HasName("PK_OrderDetailsId");
            modelBuilder.Entity<OrderItem>()
                .HasKey(e => e.Id)
                .HasName("PK_OrderItemId");
        }
        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedTime = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).ModifiedTime = DateTime.UtcNow;
            }
        }

    }
}
