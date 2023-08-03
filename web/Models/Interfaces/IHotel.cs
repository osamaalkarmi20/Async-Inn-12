using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> Create(Hotel hotel);
        Task<List<HotelDTO>> GetHotels();
        Task<HotelDTO> GetHotelId(int id);
        Task<HotelDTO> Update(int id, Hotel hotel);
        Task<HotelDTO> Delete(int id);
    }

}
