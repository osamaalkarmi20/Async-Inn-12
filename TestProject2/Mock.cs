using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace TestProject2
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(

                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();
        }
        protected async Task<Room> CreateAndSaveRoom()
        {

            var room = new Room()
            {
                Name = "TestRooms",
                Layout = 88
            };

            _db.Rooms.Add(room);

            await _db.SaveChangesAsync();

            return room;
        

        }

        protected async Task<Hotel> CreateAndSaveHotel()
        {

            var hotel = new Hotel()
            {

                Name = "TestHotel",
                City = "TestHotel",
                StreetAddress = "TestHotel",
                Phone = "TestHotel",
                Country = "TestHotel",
                State = "TestHotel"
            };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();



            return hotel;


        }
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}