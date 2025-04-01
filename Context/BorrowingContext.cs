using BorrowingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystemAPI.Context
{
    public class BorrowingContext : DbContext
    {
        public BorrowingContext(DbContextOptions<BorrowingContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Movement> Movements { get; set; }

        public DbSet<MovementType> MovementTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Filtro global en Item
            modelBuilder.Entity<Item>().HasQueryFilter(i => i.DeletedAt == null);

            // Filtro global en RequestItem (aplicar el filtro aquí también)
            modelBuilder.Entity<RequestItem>()
                .HasQueryFilter(ri => ri.Item.DeletedAt == null);


            modelBuilder.Entity<RequestItem>()
                .HasKey(ri => ri.Id);

            // Relación User -> Requests
            modelBuilder.Entity<User>()
                .HasMany(u => u.Requests)
                .WithOne(r => r.RequestedByUser)
                .HasForeignKey(r => r.RequestedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Item -> ItemMovements
            modelBuilder.Entity<Item>()
                .HasMany(i => i.ItemMovements)
                .WithOne(m => m.Item)
                .HasForeignKey(m => m.ItemId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relación Request -> RequestItems
            modelBuilder.Entity<RequestItem>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestItems)
                .HasForeignKey(ri => ri.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestItem>()
                .HasOne(ri => ri.Item)
                .WithMany()
                .HasForeignKey(ri => ri.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agregar filtros de "soft delete"
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);
            modelBuilder.Entity<Item>().HasQueryFilter(i => i.DeletedAt == null);
            modelBuilder.Entity<Movement>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<Request>().HasQueryFilter(r => r.DeletedAt == null);
            modelBuilder.Entity<RequestItem>().HasQueryFilter(ri => ri.DeletedAt == null);
            modelBuilder.Entity<MovementType>().HasQueryFilter(mt => mt.DeletedAt == null);

        }
    }
}
