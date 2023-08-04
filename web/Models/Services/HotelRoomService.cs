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
        /// <summary>
        /// Creates a new hotel room in the database and returns the corresponding HotelRoomDTO.
        /// </summary>
        /// <param name="HotelRoom">The HotelRoom object to be created.</param>
        /// <returns>The HotelRoomDTO representing the created hotel room.</returns>
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

        /// <summary>
        /// Deletes a hotel room from the database by the given hotel ID and room number, and returns the corresponding HotelRoomDTO before deletion.
        /// </summary>
        /// <param name="idHotel">The ID of the hotel to which the room belongs.</param>
        /// <param name="idRoom">The room number of the hotel room to be deleted.</param>
        /// <returns>The HotelRoomDTO representing the deleted hotel room.</returns>
        public async Task<HotelRoomDTO> Delete(int idHotel, int idRoom)

        {
            var HotelRoomDTO = await GetHotelRoomId(idHotel, idRoom);
            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();

            _context.HotelRooms.Remove(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRoomDTO;
        }
        /// <summary>
        /// Retrieves a hotel room's details from the database by the given hotel ID and room number,
        /// and returns a HotelRoomDTO representing the hotel room.
        /// </summary>
        /// <param name="idHotel">The ID of the hotel to which the room belongs.</param>
        /// <param name="idRoom">The room number of the hotel room to retrieve.</param>
        /// <returns>The HotelRoomDTO representing the requested hotel room.</returns>
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

        /// <summary>
        /// Retrieves a list of all hotel rooms from the database along with their associated rooms and amenities,
        /// and returns a list of HotelRoomDTO objects representing all hotel rooms.
        /// </summary>
        /// <returns>A list of HotelRoomDTO objects representing all hotel rooms and their amenities.</returns>
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

        /// <summary>
        /// Updates an existing hotel room in the database by the given hotel ID, room number, and new HotelRoom object.
        /// Returns the HotelRoomDTO representing the hotel room before the update.
        /// </summary>
        /// <param name="idHotel">The ID of the hotel to which the room belongs.</param>
        /// <param name="idRoom">The room number of the hotel room to update.</param>
        /// <param name="HotelRoom">The updated HotelRoom object.</param>
        /// <returns>The HotelRoomDTO representing the hotel room before the update.</returns>
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

