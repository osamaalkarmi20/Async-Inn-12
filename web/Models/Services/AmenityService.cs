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

        /// <summary>
        /// Creates a new amenity in the database and returns the corresponding AmenityDTO.
        /// </summary>
        /// <param name="aminity">The Amenity object to be created.</param>
        /// <returns>The AmenityDTO representing the created amenity.</returns>
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
        /// <summary>
        /// Deletes an amenity from the database by the given amenity ID and returns the corresponding AmenityDTO before deletion.
        /// </summary>
        /// <param name="id">The ID of the amenity to be deleted.</param>
        /// <returns>The AmenityDTO representing the deleted amenity.</returns>
        public async Task<AmenityDTO> Delete(int id)

        {
            var aminty = await GetAmenitieId(id);
            Amenity amenty = await _context.Amenities.Where(a => a.Id == aminty.Id).FirstOrDefaultAsync();
            _context.Entry(amenty).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return aminty;
        }

        /// <summary>
        /// Retrieves an amenity's details from the database by the given amenity ID and returns an AmenityDTO representing the amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to retrieve.</param>
        /// <returns>The AmenityDTO representing the requested amenity.</returns>
        public async Task<AmenityDTO> GetAmenitieId(int id)

        {

            return await _context.Amenities.Select(x => new AmenityDTO
            {
                Id = x.Id,
                Name = x.Name
            }).Where(x => x.Id == id).FirstOrDefaultAsync();


        }

        /// <summary>
        /// Retrieves a list of all amenities from the database and returns a list of AmenityDTO objects representing all amenities.
        /// </summary>
        /// <returns>A list of AmenityDTO objects representing all amenities.</returns>
        public async Task<List<AmenityDTO>> GetAmenities()

        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        /// <summary>
        /// Updates an existing amenity in the database by the given amenity ID and new Amenity object.
        /// Returns the AmenityDTO representing the amenity before the update.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="updateAmenites">The updated Amenity object.</param>
        /// <returns>The AmenityDTO representing the amenity before the update.</returns>
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
