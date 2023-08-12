using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "District Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom Hotelroom)
        {
            _hotelRoom = Hotelroom;

        }

        /// <summary>
        /// Retrieves a list of all hotel rooms from the database along with their associated hotel and room details.
        /// </summary>
        /// <returns>An ActionResult containing a list of HotelRoomDTO objects representing all hotel rooms.</returns>
        [AllowAnonymous]
        [Authorize(Roles = "Agent")]
        [Authorize(Roles = "Property Manager")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }


        /// <summary>
        /// Retrieves a hotel room's details from the database by the given hotel ID and room number,
        /// and returns a HotelRoomDTO representing the hotel room.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="roomNumber">The number of the room within the hotel.</param>
        /// <returns>An ActionResult containing the HotelRoomDTO representing the requested hotel room.</returns>
        [AllowAnonymous]
        [Authorize(Roles = "Agent")]
        [Authorize(Roles = "Property Manager")]
        [HttpGet("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomId(hotelId, roomNumber);

        }

        /// <summary>
        /// Updates an existing hotel room in the database by the given hotel ID, room number, and new HotelRoom object.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="idRoom">The number of the room within the hotel.</param>
        /// <param name="hotelRoom">The updated HotelRoom object.</param>
        /// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
        [Authorize(Roles = "Agent")]
        [Authorize(Roles = "Property Manager")]
        [HttpPut("Hotel/{hotelId}/Room/{idRoom}")]
        
        public async Task<IActionResult> PutHotelRoom(int hotelId, int idRoom, HotelRoom hotelRoom)
        {
            return Ok(await _hotelRoom.Update(hotelId, idRoom, hotelRoom));
        }
        /// <summary>
        /// Creates a new hotel room in the database and returns the corresponding HotelRoomDTO.
        /// </summary>
        /// <param name="hotelRoom">The HotelRoom object to be created.</param>
        /// <returns>An ActionResult containing the HotelRoomDTO representing the created hotel room.</returns>
        [HttpPost]
        [Authorize(Roles = "Property Manager")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoom hotelRoom)
        {
            return await _hotelRoom.Create(hotelRoom);

        }
        /// <summary>
        /// Deletes a hotel room from the database by the given hotel ID and room number,
        /// and returns the corresponding HotelRoomDTO before deletion.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to which the room belongs.</param>
        /// <param name="idRoom">The number of the room within the hotel.</param>
        /// <returns>An ActionResult containing the HotelRoomDTO representing the deleted hotel room.</returns>
        [HttpDelete("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<HotelRoomDTO> DeleteHotelRoom(int hotelId, int idRoom)
        {
            // return Ok(await _hoteRoom.Delete(hotelId, idRoom));

            return await _hotelRoom.Delete(hotelId, idRoom);
        }

    }
}