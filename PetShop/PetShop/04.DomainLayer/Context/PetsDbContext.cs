using Microsoft.EntityFrameworkCore;
using PetShop.DeveloperTesting.DomainLayer.Models;

namespace PetShop.DeveloperTesting.DomainLayer.Context
{
    public sealed class PetsDbContext: DbContext
    {
        public PetsDbContext(DbContextOptions<PetsDbContext> dbContextOptions)
           : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetCategory> PetCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetCategory>()
                .ToTable("PetCategory", "PetShop")
                .HasKey(k => k.Id).HasName("PrimaryKey_PetCategoryId");

            modelBuilder.Entity<PetCategory>().HasData(
                new PetCategory { Id = 1, Name = "Dog", Price = 25.0, Quantity = 0 },
                new PetCategory { Id = 2, Name = "Cat", Price = 15.0, Quantity = 0 },
                new PetCategory { Id = 3, Name = "Rabbit", Price = 10.0, Quantity = 0 },
                new PetCategory { Id = 4, Name = "Bird", Price = 25.0, Quantity = 0 }
                );

            modelBuilder.Entity<Pet>()
                .ToTable("Pets", "PetShop")
                .HasKey(k => k.Id).HasName("PrimaryKey_PetId");
        }
    }
}
