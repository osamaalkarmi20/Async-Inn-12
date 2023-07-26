using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
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

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
          return await _room.Get();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
          

            return await _room.GetId(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {

            if (id != room.Id)
            {
                return BadRequest();
            }

            var updateCourse = await _room.Update(id,  room);

            return  Ok(updateCourse);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {

            return await _room.Create(room);

        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id ,Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var    deleteRoom = await _room.Delete(id);

            return Ok(deleteRoom);
        }
        // post: api/2/5
        [HttpPost("{roomId}/Amenity/{amenityId}")]
       
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
          
            await _room.AddAmenityToRoom(roomId,amenityId);

            return Ok();
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]

        public async Task<IActionResult> DeleteAmenityToRoom(int roomId, int amenityId)
        {

            await _room.DeleteAmenityToRoom(roomId, amenityId);

            return Ok();
        }
    }

       
    }

