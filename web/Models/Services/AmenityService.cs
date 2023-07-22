using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.Interfaces;

namespace web.Models.Services
{
    public class AmenityService : IAmenity
    {
        

            private readonly AsyncInnDbContext _context;

            public AmenityService(AsyncInnDbContext context)
            {
                _context = context;
            }

            public async Task<Amenity> Create(Amenity amenity)
            {
                _context.Amenities.Add(amenity);

                await _context.SaveChangesAsync();


                return amenity;
            }

        public async Task<Amenity> Delete(int id)
        {
            Amenity amenity = await GetId(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return amenity;
        }

        public async Task<Amenity> GetId(int amenityId)
            {
            Amenity amenity = await _context.Amenities.FindAsync(amenityId);

                return amenity;

            }

            public async Task<List<Amenity>> Get()
            {
                var Amenities = await _context.Amenities.ToListAsync();

                return Amenities;
            }

            public async Task<Amenity> Update(int id, Amenity updatedAmenity)
        {
            Amenity amenity = await GetId(id);
            amenity.Name = updatedAmenity.Name;
        
            _context.Entry(amenity).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return amenity;
            }
        }
    }


