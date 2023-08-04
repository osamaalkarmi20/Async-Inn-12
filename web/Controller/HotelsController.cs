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
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        /// <summary>
        /// Retrieves a list of all hotels from the database along with their associated rooms and amenities.
        /// </summary>
        /// <returns>An ActionResult containing a list of HotelDTO objects representing all hotels and their rooms.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {

            return await _hotel.GetHotels();
        }

        /// <summary>
        /// Retrieves a hotel's details from the database by the given hotel ID and returns a HotelDTO representing the hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        /// <returns>An ActionResult containing the HotelDTO representing the requested hotel.</returns>
        
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            return await _hotel.GetHotelId(id);
        }

        /// <summary>
        /// Updates an existing hotel in the database by the given hotel ID and new Hotel object.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotel">The updated Hotel object.</param>
        /// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {

            return Ok(await _hotel.Update(id, hotel));
        }

        /// <summary>
        /// Creates a new hotel in the database and returns the corresponding HotelDTO.
        /// </summary>
        /// <param name="hotel">The Hotel object to be created.</param>
        /// <returns>An ActionResult containing the HotelDTO representing the created hotel.</returns>
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(Hotel hotel)
        {
            return await _hotel.Create(hotel);
        }

        /// <summary>
        /// Deletes a hotel from the database by the given hotel ID and returns the corresponding HotelDTO before deletion.
        /// </summary>
        /// <param name="id">The ID of the hotel to be deleted.</param>
        /// <returns>An IActionResult indicating the success or failure of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id, Hotel hotel)
        {


            return Ok(await _hotel.Delete(id));
        }


    }
}
