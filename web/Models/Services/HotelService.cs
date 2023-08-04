using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using web.Data;
using web.Models.DTO;
using web.Models.Interfaces;

namespace web.Models.Services
{
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Deletes a hotel from the database by the given hotel ID and returns the corresponding HotelDTO before deletion.
        /// </summary>
        /// <param name="id">The ID of the hotel to be deleted.</param>
        /// <returns>The HotelDTO representing the deleted hotel.</returns>
        public async Task<HotelDTO> Delete(int id)

        {
            var hotelDTO = await GetHotelId(id);

            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();

            _context.Hotels.Remove(hotel);

            await _context.SaveChangesAsync();

            return hotelDTO;
        }

        /// <summary>
        /// Retrieves a hotel's details from the database by the given hotel ID and returns a HotelDTO representing the hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        /// <returns>The HotelDTO representing the requested hotel.</returns>
        public async Task<HotelDTO> GetHotelId(int id)

        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();

            var hotedto = await _context.Hotels.Select(x => new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name
                ,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            }).FirstOrDefaultAsync();

            return hotedto;
        }
        /// <summary>
        /// Retrieves a list of all hotels from the database along with their associated rooms and amenities.
        /// Returns a list of HotelDTO objects representing all hotels and their rooms.
        /// </summary>
        /// <returns>A list of HotelDTO objects representing all hotels and their rooms.</returns>
        public async Task<List<HotelDTO>> GetHotels()

        {
            return await _context.Hotels
                .Include(h => h.HotelRooms)
                    .ThenInclude(hr => hr.Room)
                .Select(x => new HotelDTO
                {
                    ID = x.Id,
                    Name = x.Name,
                    StreetAddress = x.StreetAddress,
                    City = x.City,
                    State = x.State,
                    Phone = x.Phone,
                    Rooms = x.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelId,
                        RoomID = hr.RoomId,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFreindly,

                        Room = new RoomDTO
                        {
                            Id = hr.Room.Id,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout,

                            Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                            {
                                Id = ra.amenity.Id,
                                Name = ra.amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                })
            .ToListAsync();
        }


        /// <summary>
        /// Creates a new hotel in the database and returns the corresponding HotelDTO.
        /// </summary>
        /// <param name="hotel">The Hotel object to be created.</param>
        /// <returns>The HotelDTO representing the created hotel.</returns>
        public async Task<HotelDTO> Create(Hotel hotel)

        {
            var hoteldto = new HotelDTO();
            hoteldto.Name = hotel.Name;
            hoteldto.State = hotel.State;
            hoteldto.StreetAddress = hotel.StreetAddress;
            hoteldto.City = hotel.City;
            hoteldto.Phone = hotel.Phone;
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hoteldto;
        }
        /// <summary>
        /// Updates an existing hotel in the database by the given hotel ID and new Hotel object.
        /// Returns the HotelDTO representing the hotel before the update.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotel">The updated Hotel object.</param>
        /// <returns>The HotelDTO representing the hotel before the update.</returns>
        public async Task<HotelDTO> Update(int id, Hotel hotel)
        {
            var hotels = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();

            var hotelupdata = new HotelDTO
            {
                ID = id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,

            };

            hotels.Name = hotel.Name;
            hotels.State = hotel.State;
            hotels.Phone = hotel.Phone;
            hotels.City = hotel.City;
            hotels.Country = hotel.Country;


            _context.Entry(hotels).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotelupdata;
        }
    }
}


