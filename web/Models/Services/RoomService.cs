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

        /// <summary>
        /// Creates a new room in the database and returns the corresponding RoomDTO.
        /// </summary>
        /// <param name="room">The Room object to be created.</param>
        /// <returns>The RoomDTO representing the created room.</returns>
       

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
        /// <summary>
        /// Deletes a room from the database by the given room ID and returns the corresponding RoomDTO before deletion.
        /// </summary>
        /// <param name="id">The ID of the room to be deleted.</param>
        /// <returns>The RoomDTO representing the deleted room.</returns>


        public async Task<RoomDTO> Delete(int id)
        {
            RoomDTO roomDto = await GetRoomId(id);
            Room room = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();


            return roomDto;

        }
        /// <summary>
        /// Retrieves a room's details from the database by the given room ID and returns a RoomDTO representing the room.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>The RoomDTO representing the requested room.</returns>
      

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
        /// <summary>
        /// Retrieves a list of all rooms from the database along with their associated amenities, and returns a list of RoomDTOs.
        /// </summary>
        /// <returns>A list of RoomDTO objects representing all rooms.</returns>
       
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
        /// <summary>
        /// Updates an existing room in the database by the given room ID and new Room object.
        /// Returns the RoomDTO representing the room before the update.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated Room object.</param>
        /// <returns>The RoomDTO representing the room before the update.</returns>
      


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
        /// <summary>
        /// Adds an amenity to a room in the database and returns the corresponding RoomAmenity object.
        /// </summary>
        /// <param name="roomId">The ID of the room to which the amenity will be added.</param>
        /// <param name="amenityId">The ID of the amenity to add to the room.</param>
        /// <returns>The RoomAmenity object representing the added association between the room and amenity.</returns>
        


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
        /// <summary>
        /// Removes an amenity from a room in the database and returns the corresponding RoomAmenity object.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the amenity will be removed.</param>
        /// <param name="amenityId">The ID of the amenity to remove from the room.</param>
        /// <returns>The RoomAmenity object representing the removed association between the room and amenity.</returns>
 
        public async Task<RoomAmenity> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var roomameneties = await _context.AmeRoomAmenitiesnities.Where(x => x.RoomId == roomId && x.AmenityId == amenityId).FirstOrDefaultAsync();
            _context.Entry(roomameneties).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return roomameneties;
        }
    }
    }



