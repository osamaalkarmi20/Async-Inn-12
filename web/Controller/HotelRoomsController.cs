using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Models.DTO;
using web.Models.Interfaces;
using web.Models.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom Hotelroom)
        {
            _hotelRoom = Hotelroom;

        }

        // GET: api/HotelRooms
        [HttpGet]

        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomId(hotelId, roomNumber);

        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int idRoom, HotelRoom hotelRoom)
        {
            return Ok(await _hotelRoom.Update(hotelId, idRoom, hotelRoom));
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoom hotelRoom)
        {
            return await _hotelRoom.Create(hotelRoom);

        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<HotelRoomDTO> DeleteHotelRoom(int hotelId, int idRoom)
        {
            // return Ok(await _hoteRoom.Delete(hotelId, idRoom));

            return await _hotelRoom.Delete(hotelId, idRoom);
        }

    }
}