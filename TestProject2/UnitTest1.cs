using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Models.Services;

namespace TestProject2

{
    public class UnitTest1 : Mock
    {


        [Fact]
        public async void TestAddRoom()
        {
            var room = await CreateAndSaveRoom();

            var roomservics = new RoomService(_db);

            var roomid = await roomservics.GetRoomId(room.Id);

            Assert.Equal(4, roomid.Id);

        }
        [Fact]
        public async Task TestGetRooms()
        {
            var roomService = new RoomService(_db);
            var rooms = await roomService.GetRooms();

            Assert.NotNull(rooms);
            Assert.NotEmpty(rooms);
        }

        [Fact]
        public async void TestUpdateRoom()
        {
            var room = await CreateAndSaveRoom();

            var roomService = new RoomService(_db);
            var updatedRoom = await roomService.Update(room.Id, new Room
            {
                Name = "Updated Room",
                Layout = 2
            });
            var roomupdated = await roomService.GetRoomId(room.Id);
            Assert.NotNull(updatedRoom);
            Assert.Equal("Updated Room", roomupdated.Name);
            Assert.Equal(2, roomupdated.Layout);
     
        }

        [Fact]
        public async void TestDeleteRoom()
        {
            var room = await CreateAndSaveRoom();

            var roomService = new RoomService(_db);
            var deletedRoom = await roomService.Delete(room.Id);

            Assert.NotNull(deletedRoom);
            Assert.Equal(room.Id, deletedRoom.Id);
        }

        [Fact]
        public async void TestAddHotel()
        {
            var Hotel = await CreateAndSaveHotel();

            var Hotelservics = new HotelServices(_db);

            var Hotelid = await Hotelservics.GetHotelId(Hotel.Id);

            Assert.NotEqual(0, Hotelid.ID);

        }

        [Fact]
        public async void TestCreateHotel()
        {
            var hotel = new Hotel()
            {
                Name = "Test Hotel",
                City = "Test City",
                StreetAddress = "Test Street",
                Phone = "1234567890",
                Country = "Test Country",
                State = "Test State"
            };

            var hotelService = new HotelServices(_db);
            var createdHotel = await hotelService.Create(hotel);

            Assert.NotNull(createdHotel);
            Assert.Equal("Test Hotel", createdHotel.Name);
        }

        [Fact]
        public async void TestGetHotels()
        {
            var hotelService = new HotelServices(_db);
            var hotels = await hotelService.GetHotels();

            Assert.NotNull(hotels);


        }

        [Fact]
        public async void TestUpdateHotel()
        {
            var hotel = await CreateAndSaveHotel();

            var hotelService = new HotelServices(_db);

            var updatedHotel = await hotelService.Update(hotel.Id, new Hotel
            {
                Name = "Updated Hotel",
                City = "Updated City",
                StreetAddress = "Updated Street",
                Phone = "0987654321",
                Country = "Updated Country",
                State = "Updated State"
            });
            var hotelupdated = await hotelService.GetHotelId(hotel.Id);

            Assert.NotNull(updatedHotel);
            Assert.Equal("Updated Hotel", hotelupdated.Name);
            Assert.Equal("Updated City", hotelupdated.City);

        }

        [Fact]
        public async void TestDeleteHotel()
        {
            var hotel = await CreateAndSaveHotel();

            var hotelservice = new HotelServices(_db);
            var deletedhotel = await hotelservice.Delete(hotel.Id);

            Assert.NotNull(deletedhotel);
            Assert.Equal(hotel.Id, deletedhotel.ID);
        }

    }
}