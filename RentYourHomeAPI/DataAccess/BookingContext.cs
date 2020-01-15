using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentYourHomeAPI.Models;

namespace RentYourHomeAPI.DataAccess
{
    public class BookingContext : DbContext, IBookingContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Home> Homes { get; set; }

        public void Remove(Home home) 
        {
            base.Remove(home);
        }

        public Task SaveChangesAsync()
        {
           return base.SaveChangesAsync();
        }

        public void Update(Home home)
        {
            base.Update(home);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Owner>().HasKey(m => m.Id);
            modelBuilder.Entity<Owner>().ToTable("Owners");

            modelBuilder.Entity<Home>().HasKey(m => m.Id);
            modelBuilder.Entity<Home>().ToTable("Homes");

            modelBuilder.Entity<Owner>()
                .HasData(
                    new Owner
                    {
                        Id = 1,
                        Name = "Benito",
                        Password = "pepe"
                    },
                    new Owner
                    {
                        Id = 2,
                        Name = "Pepe",
                        Password = "lucas"
                    },
                    new Owner
                    {
                        Id = 3,
                        Name = "Lucas",
                        Password = "maria"
                    });

            modelBuilder.Entity<Home>()
                .HasData(
                    new Home
                    {
                        Id = 1,
                        OwnerId = 1,
                        Name = "La Campana",
                        Stars = 5,
                        City = "Motril",
                        Description = "El mejor hotel en la Costa Tropical",
                        Imagen = "http://localhost:62122/imgs/campana1.jpg"
                    },
                    new Home
                    {
                        Id = 2,
                        OwnerId = 1,
                        Name = "Bahia Málaga",
                        Stars = 4,
                        City = "Málaga",
                        Description = "El mejor hotel en la Costa del Sol",
                        Imagen = "http://localhost:62122/imgs/bahia1.jpg"
                    },
                    new Home
                    {
                        Id = 3,
                        OwnerId = 2,
                        Name = "Teatinos Alto",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    });

            base.OnModelCreating(modelBuilder);

        }

    }
}
