using Microsoft.EntityFrameworkCore;
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

        public async Task<HotelRoom> Create(int hotelId, int roomId, decimal rate, bool petfreindly)
        {
            HotelRoom CREATED = new HotelRoom()
            {
                
                HotelId = hotelId,
                RoomId = roomId,
                RoomNumber = roomId * 100 + hotelId,
                Rate = rate,
                PetFreindly = petfreindly,
                Room = await _room.GetId(hotelId),
                Hotel = await _hotel.GetId(hotelId)


            };

      

          _context.HotelRooms.Add(CREATED);

            await _context.SaveChangesAsync();


            return CREATED;
        }
        public async Task<List<HotelRoom>> Get(int hotelId)
        {

            
            var hotelRooms = await _context.HotelRooms.ToListAsync();



            return hotelRooms;
        }
        public async Task<HotelRoom> GetId(int hotelId,int RoomNumber)
        {


            var hotelRooms = await _context.HotelRooms
          .Where(x => x.RoomNumber == RoomNumber && x.HotelId == hotelId)
          .Include(x => x.RoomId).Include(x => x.Room.RoomAmenities)
          .FirstAsync();

            return hotelRooms;
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


