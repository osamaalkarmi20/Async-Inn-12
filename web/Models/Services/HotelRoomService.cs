using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using web.Data;
using web.Models;
using web.Models.DTO;
using web.Models.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {


        private readonly AsyncInnDbContext _context;
    
        public HotelRoomService(AsyncInnDbContext context)
        {
            _context = context;
          
        }
        public async Task<HotelRoomDTO> Create(HotelRoom HotelRoom)
        {
            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = HotelRoom.HotelId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                Room = await _context.Rooms.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync(),
                Hotel = await _context.Hotels.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync()
            };
            var HotelDto = new HotelRoomDTO()
            {
                HotelID = HotelRoom.HotelId,
                RoomID = HotelRoom.RoomId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly = HotelRoom.PetFreindly,

            };

            _context.HotelRooms.Add(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelDto;
        }

        public async Task<HotelRoomDTO> Delete(int idHotel, int idRoom)
        {
            var HotelRoomDTO = await GetHotelRoomId(idHotel, idRoom);
            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();

            _context.HotelRooms.Remove(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRoomDTO;
        }
        public async Task<HotelRoomDTO> GetHotelRoomId(int idHotel, int idRoom)
        {

            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            var HotelRoomDTO = new HotelRoomDTO()
            {
                HotelID = HotelRoom.HotelId,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly = HotelRoom.PetFreindly,
                Rate = HotelRoom.Rate,
                RoomID = HotelRoom.RoomId,

            };

            return HotelRoomDTO;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            return await _context.HotelRooms
                .Include(hr => hr.Hotel)
                .Include(hr => hr.Room).ThenInclude(r => r.RoomAmenities).ThenInclude(ra => ra.amenity)
                .Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelId,
                    RoomID = x.RoomId,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFreindly,

                    Room = new RoomDTO
                    {
                        Id = x.Room.Id,
                        Name = x.Room.Name,

                        Amenities = x.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            Id = ra.amenity.Id,
                            Name = ra.amenity.Name
                        }).ToList()
                    },


                })
                .ToListAsync();
        }

        public async Task<HotelRoomDTO> Update(int idHotel, int idRoom, HotelRoom HotelRoom)
        {
            var hotels = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();

            var Temproom = await GetHotelRoomId(idHotel, idRoom);
            Temproom.RoomNumber = HotelRoom.RoomNumber;
            Temproom.Rate = HotelRoom.Rate;
            Temproom.PetFriendly = HotelRoom.PetFreindly;

            hotels.HotelId = HotelRoom.HotelId;
            hotels.RoomId = HotelRoom.RoomId;
            hotels.RoomNumber = HotelRoom.RoomNumber;
            hotels.Rate = HotelRoom.Rate;
            hotels.PetFreindly = HotelRoom.PetFreindly;

            _context.Entry(hotels).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temproom;

        }
    }
}

