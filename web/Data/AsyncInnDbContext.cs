using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Numerics;
using web.Models;
using web.Models.Interfaces;

namespace web.Data
{
    public class AsyncInnDbContext: IdentityDbContext<ApplicationUser>
    {
        public AsyncInnDbContext(DbContextOptions options): base(options)
        {
  
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "cont", StreetAddress="23.st", City ="NY", State ="NY", Country ="US", Phone ="1233442"},
                new Hotel() { Id = 2, Name = "res"  ,StreetAddress = "25.st", City = "LA", State = "LA", Country = "US", Phone = "1233444442" },
                new Hotel() { Id = 3, Name = "hunt"  ,StreetAddress = "28.st", City = "TX", State = "TX", Country = "US", Phone = "123223442" }
                );
            modelBuilder.Entity<Room>().HasData(
              new Room() { Id = 1, Name = "honey" , Layout = 1 },
              new Room() { Id = 2, Name = "red" , Layout = 2 },
              new Room() { Id = 3, Name = "white" , Layout = 3 }
              );
            modelBuilder.Entity<Amenity>().HasData(
              new Amenity() { Id = 1, Name = "AC" },
              new Amenity() { Id = 2, Name = "coffeeBar" },
              new Amenity() { Id = 3, Name = "Fridge"}
              );

            modelBuilder.Entity<RoomAmenity>().HasKey(
                roomamenity => new { roomamenity.RoomId, roomamenity.AmenityId });
            modelBuilder.Entity<HotelRoom>().HasKey(
            hotelroom => new { hotelroom.RoomNumber, hotelroom.HotelId });



            seedRole(modelBuilder, "District Manager");
            seedRole(modelBuilder, "Property Manager");
            seedRole(modelBuilder, "Agent");


        }

        private void seedRole(ModelBuilder modelBuilder, string roleName)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms{ get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> AmeRoomAmenitiesnities { get; set; }
     
    }
}
