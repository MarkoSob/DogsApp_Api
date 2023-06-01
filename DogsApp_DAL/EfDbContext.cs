using DogsApp_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsApp_DAL
{
    public class EfDbContext : DbContext
    {
        public DbSet<Dog>? Dogs { get; set; }
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().HasData(
                new Dog
                {
                    Id = Guid.Parse("9d25f40b-68de-4e7f-b76b-74f87f26f654"),
                    Name = "Neo",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32

                },
               new Dog
               {
                   Id = Guid.Parse("44f8f00a-17e6-46d9-9e0c-be3326514c02"),
                   Name = "Jessy",
                   Color = "black & white",
                   TailLength = 7,
                   Weight = 14
               });
        }
    }
}
