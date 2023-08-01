using web.Models.DTO;

namespace web.Models.Interfaces

{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(Amenity amenity);
        Task<List<AmenityDTO>> Get();
        Task<AmenityDTO> GetId(int amenityId);

        Task<AmenityDTO> Update(int id, Amenity updatedAmenity );
        Task<AmenityDTO> Delete(int id);

    }
}
