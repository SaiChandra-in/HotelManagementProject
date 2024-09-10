using Hotel.DTO;
using Hotel.Models;
using Hotel.Services;
using Microsoft.EntityFrameworkCore;
//using Hotel.DTO;

namespace Hotel.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        { 
        }
        public ApplicationDbContext()
        {
        }
        public virtual DbSet<Admin> Admins { get; set; }
        
        public virtual DbSet<Guest> Guests { get; set; }

        public virtual DbSet<Inventory> Inventories { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Staff> Staffs { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Password_Hash a = new Password_Hash();
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                AdminId = 1,
                AdminName = "Sai Chandra",
                Email = "chandra@gmail.com",
                Role = "Owner",
                Password = a.HashPassword("Chandra@123"),
                ConfirmPassword = a.HashPassword("Chandra@123")
            });
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                AdminId = 2,
                AdminName = "Sameer",
                Email = "sameer@gmail.com",
                Role = "Manager",
                Password = a.HashPassword("Sameer@123"),
                ConfirmPassword = a.HashPassword("Sameer@123")
            });
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                AdminId = 3,
                AdminName = "Ashok",
                Email = "ashok@gmail.com",
                Role = "Receptionist",
                Password = a.HashPassword("Ashok@123"),
                ConfirmPassword = a.HashPassword("Ashok@123")
            });

        }
        //public DbSet<Hotel.DTO.ReservationDTO> ReservationDTO { get; set; } = default!;


    }
}
