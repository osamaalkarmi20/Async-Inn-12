using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<HotelRoomDTO> Create(HotelRoom hotelRoom);
        Task<List<HotelRoomDTO>> GetHotelRooms();
        Task<HotelRoomDTO> GetHotelRoomId(int hotelId, int roomId);
        Task<HotelRoomDTO> Update(int hotelId, int roomId, HotelRoom hotelRoom);
        Task<HotelRoomDTO> Delete(int hotelId, int roomId);
    }
}
