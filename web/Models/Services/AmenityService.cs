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

            public async Task<AmenityDTO> Create(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            AmenityDTO amenityDTO = new AmenityDTO()
           {
               Id = amenity.Id,
               Name = amenity.Name
           };


                await _context.SaveChangesAsync();


                return amenityDTO;
            }

        public async Task<AmenityDTO> Delete(int id)
        {
            AmenityDTO amenityDTO = await GetId(id);
            Amenity amenity = new Amenity();
            amenity.Id = amenityDTO.Id ;
            amenity.Name = amenityDTO.Name;



            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return amenityDTO;
        }

        public async Task<AmenityDTO> GetId(int amenityId)
        { AmenityDTO amenity = await _context.Amenities.Select(amenity => new AmenityDTO
        {
            Id = amenity.Id,
            Name = amenity.Name

        }).FirstOrDefaultAsync(s => s.Id == amenityId);

            return amenity;

            }

            public async Task<List<AmenityDTO>> Get()
            {
        {
            return  await _context.Amenities.Select(amenity => new AmenityDTO
            {
                Id = amenity.Id,
                Name = amenity.Name

            }).ToListAsync();

           

        }
    }



public async Task<AmenityDTO> Update(int id, Amenity updatedAmenity)
        {
            
           
          
            var amenityDTO = await GetId(id);

            Amenity amenity = await _context.Amenities.Where(a => a.Id == id).FirstAsync();

            amenity.Name = updatedAmenity.Name;
            amenity.Id = id;

            _context.Entry(amenity).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return amenityDTO;
            }
        }
    }


