using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IRoom
    {

        Task<RoomDTO> Create(Room room);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoomId(int id);
        Task<RoomDTO> Update(int id, Room room);
        Task<RoomDTO> Delete(int id);

        Task<RoomAmenity> AddAmenityToRoom(int roomId, int amenityId);
        Task<RoomAmenity> RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
