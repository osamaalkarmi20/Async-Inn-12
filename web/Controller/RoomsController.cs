using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Models.DTO;
using web.Models.Interfaces;

namespace web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        /// <summary>
        /// Retrieves a list of all rooms from the database along with their associated amenities.
        /// </summary>
        /// <returns>An ActionResult containing a list of RoomDTO objects representing all rooms.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {

            return await _room.GetRooms();
        }

        /// <summary>
        /// Retrieves a room's details from the database by the given room ID and returns a RoomDTO representing the room.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>An ActionResult containing the RoomDTO representing the requested room.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _room.GetRoomId(id);
        }

        /// <summary>
        /// Updates an existing room in the database by the given room ID and new Room object.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated Room object.</param>
        /// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            return Ok(await _room.Update(id, room));
        }

        /// <summary>
        /// Creates a new room in the database and returns the corresponding RoomDTO.
        /// </summary>
        /// <param name="room">The Room object to be created.</param>
        /// <returns>An ActionResult containing the RoomDTO representing the created room.</returns>
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(Room room)
        {
            return await _room.Create(room);
        }

        /// <summary>
        /// Deletes a room from the database by the given room ID and returns the corresponding RoomDTO before deletion.
        /// </summary>
        /// <param name="id">The ID of the room to be deleted.</param>
        /// <returns>An IActionResult indicating the success or failure of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            return Ok(await _room.Delete(id));
        }
        /// <summary>
        /// Adds an amenity to a room in the database and returns the corresponding RoomAmenity object.
        /// </summary>
        /// <param name="roomId">The ID of the room to which the amenity will be added.</param>
        /// <param name="amenityId">The ID of the amenity to add to the room.</param>
        /// <returns>A RoomAmenity object representing the added association between the room and amenity.</returns>
        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmenity> PostRoomaded(int roomId, int amenityId)
        {
            return await _room.AddAmenityToRoom(roomId, amenityId);
        }

        /// <summary>
        /// Removes an amenity from a room in the database and returns the corresponding RoomAmenity object.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the amenity will be removed.</param>
        /// <param name="amenityId">The ID of the amenity to remove from the room.</param>
        /// <returns>A RoomAmenity object representing the removed association between the room and amenity.</returns>
        
        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmenity> DeleteRoomaded(int roomId, int amenityId)
        {
            return await _room.RemoveAmentityFromRoom(roomId, amenityId);
        }
    }
    }

