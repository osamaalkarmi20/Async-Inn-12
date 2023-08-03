using web.Models.DTO;

namespace web.Models.Interfaces

{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(Amenity aminity);
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> GetAmenitieId(int id);
        Task<AmenityDTO> Update(int id, Amenity amenity);
        Task<AmenityDTO> Delete(int id);

    }
}
