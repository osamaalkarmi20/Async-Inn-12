using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(Room room);
        Task<List<RoomDTO>> Get();
        Task<RoomDTO> GetId(int roomId);

        Task<RoomDTO> Update(int id, Room updatedRoom);
        Task<RoomDTO> Delete(int id);
        Task AddAmenityToRoom(int roomId, int amenityId);

        Task<RoomAmenity> DeleteAmenityToRoom(int roomId, int amenityId);

    }
}
