using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
            var RoomDTO = new RoomDTO()
            {
                Id = room.Id,
                Name = room.Name,
                Layout = room.Layout,
                 Amenities = _context.AmeRoomAmenitiesnities.Select(r => new AmenityDTO
                {
                    Id = r.amenity.Id,
                    Name = r.amenity.Name,
                }).ToList()

            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return RoomDTO;
        }

        public async Task<RoomDTO> Delete(int id)
        {
            RoomDTO roomDto = await GetRoomId(id);
            Room room = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();


            return roomDto;

        }

        public async Task<RoomDTO> GetRoomId(int id)
        {
            var Room = await _context.Rooms.Where(X => X.Id == id).FirstOrDefaultAsync();


            var RoomDto = await _context.Rooms.Select(x => new RoomDTO
            {
                Id = id,
                Name = Room.Name,
                Layout = Room.Layout,
                Amenities = x.RoomAmenities.Select(r => new AmenityDTO
                {
                    Id = r.amenity.Id,
                    Name = r.amenity.Name,
                }).ToList()
            }).FirstOrDefaultAsync();

            return RoomDto;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {

            return await _context.Rooms
      .Include(r => r.RoomAmenities)
      .ThenInclude(ra => ra.amenity)
      .Select(x => new RoomDTO
      {
          Id = x.Id,
          Name = x.Name,
          Layout = x.Layout,
          Amenities = x.RoomAmenities.Select(h => new AmenityDTO
          {
              Id = h.amenity.Id,
              Name = h.amenity.Name,
          }).ToList()
      })
      .ToListAsync();
        }

        public async Task<RoomDTO> Update(int id, Room room)
        {
            RoomDTO roomDto = await GetRoomId(id);

            Room Temproom = await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
          
            Temproom.Layout = room.Layout;
            Temproom.Name = room.Name;

            _context.Entry(Temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDto;

           
           
            
            
        }


        public async Task<RoomAmenity> AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmeneties = new RoomAmenity()
            {
                RoomId = roomId,
                AmenityId = amenityId,
                room = await _context.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync(),
                amenity = await _context.Amenities.Where(x => x.Id == amenityId).FirstOrDefaultAsync()


            };

            _context.AmeRoomAmenitiesnities.Add(roomAmeneties);

            await _context.SaveChangesAsync();

            return roomAmeneties;
        }

        public async Task<RoomAmenity> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var roomameneties = await _context.AmeRoomAmenitiesnities.Where(x => x.RoomId == roomId && x.AmenityId == amenityId).FirstOrDefaultAsync();
            _context.Entry(roomameneties).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return roomameneties;
        }
    }
    }



