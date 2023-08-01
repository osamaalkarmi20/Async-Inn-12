using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.DTO;
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

            public async Task<RoomDTO> Create(Room room)
            {
                _context.Rooms.Add(room);

            RoomDTO roomDTO = new RoomDTO()
            {
                
            Id= room.Id,
            Name = room.Name,

           Layout=room.Layout };

            
                      await _context.SaveChangesAsync();


                return roomDTO;
            }

        public async Task<RoomDTO> Delete(int id)
        {
            RoomDTO roomDTO  = await GetId(id);
            Room room = new Room();
            room.Id = roomDTO.Id;
            room.Name = roomDTO.Name;
            room.Layout = roomDTO.Layout;
            room.RoomAmenities = await _context.AmeRoomAmenitiesnities.Select(amenity => new RoomAmenity { AmenityId = amenity.AmenityId,RoomId=amenity.RoomId,amenity=amenity.amenity,room=amenity.room}).ToListAsync();
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return roomDTO;
        }

        public async Task<RoomDTO> GetId(int roomId)
        {
            Room room = await _context.Rooms.Where(x => x.Id == roomId).Include(x => x.RoomAmenities).FirstAsync();
            RoomDTO roomDTO = new RoomDTO()
            {
                Id = roomId,
                Name = room.Name,
                Layout = room.Layout,
                Amenities = await _context.AmeRoomAmenitiesnities.Select(amenity => new AmenityDTO { Id = amenity.amenity.Id, Name = amenity.amenity.Name }).ToListAsync()



            };
                return roomDTO;

            }

            public async Task<List<RoomDTO>> Get()
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


