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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {

            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _room.GetRoomId(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            return Ok(await _room.Update(id, room));
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(Room room)
        {
            return await _room.Create(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            return Ok(await _room.Delete(id));
        }

        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmenity> PostRoomaded(int roomId, int amenityId)
        {
            return await _room.AddAmenityToRoom(roomId, amenityId);
        }

        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmenity> DeleteRoomaded(int roomId, int amenityId)
        {
            return await _room.RemoveAmentityFromRoom(roomId, amenityId);
        }
    }
    }

