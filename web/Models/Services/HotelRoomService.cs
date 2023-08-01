using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using web.Data;
using web.Models.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {


        private readonly AsyncInnDbContext _context;
        private readonly IRoom _room;
        private readonly IHotel _hotel;

        public HotelRoomService(AsyncInnDbContext context, IRoom room, IHotel hotel)
        {
            _context = context;
            _room = room;
            _hotel = hotel;
        }

        public async Task<HotelRoom> Create(int hotelId, HotelRoom hotelRoom)
        {

            HotelRoom CREATED= new HotelRoom();

         
         
            CREATED.HotelId = hotelId;
            CREATED.RoomId = hotelRoom.RoomId;
            CREATED.PetFreindly = hotelRoom.PetFreindly;
            CREATED.Rate = hotelRoom.Rate;
            CREATED.RoomNumber = hotelRoom.RoomNumber;
            CREATED.Room = await _room.GetId(CREATED.RoomId);
            CREATED.Hotel = await _hotel.GetId(hotelRoom.HotelId);
            _context.Entry(CREATED).State = EntityState.Added;
            await _context.SaveChangesAsync();


            return CREATED;
        }
        public async Task<List<HotelRoom>> Get(int hotelId)
        {

            
            var hotelRooms = await _context.HotelRooms.Include(x => x.Room.RoomAmenities).Include(x => x.Hotel).ToListAsync();



            return hotelRooms;
        }
        public async Task<HotelRoom> GetId(int hotelId, int RoomNumber)
        {


            var hotelRooms = await _context.HotelRooms
          .Where(x => x.RoomNumber == RoomNumber &&  x.HotelId == hotelId).Include(x => x.Room)
          .Include(x => x.Room.RoomAmenities).Include(x => x.Hotel)
          .FirstAsync();

            return hotelRooms;
        }
        public async Task<HotelRoom> UpdateRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            //var hotelRoom = await _context.room_amenities.FindAsync(hotelId, roomNumber);
            var Temproom = await GetId(hotelId, roomNumber);
            Temproom.RoomNumber = roomNumber;
            Temproom.Rate = hotelRoom.Rate;
            Temproom.PetFreindly = hotelRoom.PetFreindly;
            _context.Entry(Temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temproom;
        }


        public async Task<HotelRoom> Delete(int hotelId, int RoomNumber)
        {
           

            var hotelroom = await _context.HotelRooms.Where(x => x.RoomNumber == RoomNumber && x.HotelId == hotelId).FirstAsync();

            _context.Entry(hotelroom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return hotelroom;


        }

       

    }
}


