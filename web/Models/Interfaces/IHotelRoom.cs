using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(int hotelId ,HotelRoom hotelRoom);

        Task<List<HotelRoom>> Get(int hotelId);
        Task<HotelRoom> GetId(int hotelId, int RoomNumber);
        Task<HotelRoom> UpdateRoom(int hotelId, int roomNumber, HotelRoom hotelRoom);
        Task<HotelRoom> Delete(int hotelId, int RoomNumber);
 
    }
}
