using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using web.Data;
using web.Models.DTO;
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

        public async Task<AmenityDTO> Create(Amenity aminity)
        {
            AmenityDTO amenityDto = new AmenityDTO()
            {
                Id = aminity.Id,
                Name = aminity.Name,
            };

            _context.Amenities.Add(aminity);
            await _context.SaveChangesAsync();
            return amenityDto;
        }
        public async Task<AmenityDTO> Delete(int id)
        {
            var aminty = await GetAmenitieId(id);
            Amenity amenty = await _context.Amenities.Where(a => a.Id == aminty.Id).FirstOrDefaultAsync();
            _context.Entry(amenty).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return aminty;
        }

        public async Task<AmenityDTO> GetAmenitieId(int id)
        {

            return await _context.Amenities.Select(x => new AmenityDTO
            {
                Id = x.Id,
                Name = x.Name
            }).Where(x => x.Id == id).FirstOrDefaultAsync();


        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<AmenityDTO> Update(int id, Amenity updateAmenites)
        {
            var amenty = await GetAmenitieId(id);
            Amenity amenity = await _context.Amenities.Where(a => a.Id == id).FirstOrDefaultAsync();
            amenity.Name = updateAmenites.Name;
            amenity.Id = id;
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenty;
        }
    }
}
