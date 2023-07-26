namespace web.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(int hotelId , int roomId,decimal rate,bool petfreindly);

        Task<List<HotelRoom>> Get(int hotelId);
        Task<HotelRoom> GetId(int hotelId, int RoomNumber);

        Task<HotelRoom> Delete(int hotelId, int RoomNumber);
 
    }
}
