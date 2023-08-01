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

        // GET:/api/Hotels/{hotelId}/Rooms
        [HttpGet]
        [Route("Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<List<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            return await _hotelRoom.Get(hotelId);
        }


        [HttpPost]
        [Route("Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, HotelRoom hotelRoom)
        {

            return await _hotelRoom.Create(hotelId, hotelRoom);

        }
        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelId}/Rooms/{RoomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId,int RoomNumber)
        {



            return await _hotelRoom.GetId(hotelId, RoomNumber);
        }

        [HttpPut]
        [Route("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            try
            {
                await _hotelRoom.UpdateRoom(hotelId, roomNumber, hotelRoom);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "An error occurred while updating the hotel room.");
            }
        }

        // POST: "/Hotels/{hotelId}/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       

        // DELETE: api/HotelRooms/5
        [HttpDelete]
        [Route("Hotels/{hotelId}/Rooms/{RoomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int RoomNumber)
        {
            await _hotelRoom.Delete(hotelId, RoomNumber);
            {
                return Ok();
            }


        }
    }
}
