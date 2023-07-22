﻿namespace web.Models.Interfaces
{
    public interface IAmenity
    {
        Task<Amenity> Create(Amenity amenity);
        Task<List<Amenity>> Get();
        Task<Amenity> GetId(int amenityId);

        Task<Amenity> Update(int id, Amenity updatedAmenity );
        Task<Amenity> Delete(int id);

    }
}
