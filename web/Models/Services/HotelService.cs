using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.Interfaces;

namespace web.Models.Services
{
    public class HotelService : IHotel
    {
        

            private readonly AsyncInnDbContext _context;

            public HotelService(AsyncInnDbContext context)
            {
                _context = context;
            }

            public async Task<Hotel> Create(Hotel hotel)
            {
                _context.Hotels.Add(hotel);

                await _context.SaveChangesAsync();


                return hotel;
            }

        public async Task<Hotel> Delete(int id)

        {
            Hotel hotel = await GetId(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return hotel;
        }

        public async Task<Hotel> GetId(int hotelId)
            {
            Hotel hotel = await _context.Hotels.FindAsync(hotelId);

                return hotel;

            }

            public async Task<List<Hotel>> Get()
            {
                var hotels = await _context.Hotels.ToListAsync();

                return hotels;
            }

            public async Task<Hotel> Update(int id, Hotel updateedHotel)
            {
            Hotel hotel = await GetId(id);
            hotel.Name = updateedHotel.Name;
            hotel.StreetAddress = updateedHotel.StreetAddress;
            hotel.City = updateedHotel.City;
            hotel.State = updateedHotel.State;
            hotel.Country = updateedHotel.Country;
            hotel.Phone = updateedHotel.Phone;
            _context.Entry(hotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return hotel;
            }
        }
    }


