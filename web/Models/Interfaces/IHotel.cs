namespace web.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);
        Task<List<Hotel>> Get();
        Task<Hotel> GetId(int hotelId);

        Task<Hotel> Update(int id);
        Task<Hotel> Delete(int id);

    }
}
