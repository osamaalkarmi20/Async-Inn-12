namespace web.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);
        Task<List<Room>> Get();
        Task<Room> GetId(int roomId);

        Task<Room> Update(int id);
        Task<Room> Delete(int id);

    }
}
