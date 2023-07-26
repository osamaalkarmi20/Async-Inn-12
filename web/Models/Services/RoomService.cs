using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.Interfaces;

namespace web.Models.Services
{
    public class RoomService: IRoom
    {
        

            private readonly AsyncInnDbContext _context;

            public RoomService(AsyncInnDbContext context)
            {
                _context = context;
            }

            public async Task<Room> Create(Room room)
            {
                _context.Rooms.Add(room);

                await _context.SaveChangesAsync();


                return room;
            }

        public async Task<Room> Delete(int id)
        {
            Room room  = await GetId(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return room;
        }

        public async Task<Room> GetId(int roomId)
        {
            Room room = await _context.Rooms.Where(x => x.Id==roomId).Include(x=> x.RoomAmenities).FirstAsync();

                return room;

            }

            public async Task<List<Room>> Get()
            {
                var rooms = await _context.Rooms.Include(x=> x.RoomAmenities).ToListAsync();

                return rooms;
            }

            public async Task<Room> Update(int id, Room updatedRoom)
        {

            Room room = await GetId(id);

                 room.Name = updatedRoom.Name;
                 room.Layout = updatedRoom.Layout;
              
                _context.Entry(room).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return room;
            }


      
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                AmenityId = amenityId,
                RoomId = roomId,
              
            };

            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();

        }
        public async Task<RoomAmenity> DeleteAmenityToRoom(int roomId, int amenityId)
        {
            var roomAmenity = await _context.AmeRoomAmenitiesnities.Where(x=>x.RoomId==roomId && x.AmenityId==amenityId).FirstAsync();

         _context.Entry(roomAmenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return roomAmenity;
        }
    }
}


