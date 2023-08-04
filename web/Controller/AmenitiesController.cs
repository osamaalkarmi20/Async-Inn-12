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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _amenity;

        public AmenitiesController(IAmenity amenity)
        {
            _amenity = amenity;
        }
        /// <summary>
        /// Retrieves a list of all amenities from the database.
        /// </summary>
        /// <returns>An ActionResult containing a list of AmenityDTO objects representing all amenities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {

            return await _amenity.GetAmenities();
        }
        /// <summary>
        /// Retrieves an amenity's details from the database by the given amenity ID and returns an AmenityDTO representing the amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to retrieve.</param>
        /// <returns>An ActionResult containing the AmenityDTO representing the requested amenity.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenities(int id)
        {


            return await _amenity.GetAmenitieId(id);
        }

        /// <summary>
        /// Updates an existing amenity in the database by the given amenity ID and new Amenity object.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="amenities">The updated Amenity object.</param>
        /// <returns>An IActionResult indicating the success or failure of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, Amenity amenities)
        {

            return Ok(await _amenity.Update(id, amenities));
        }
        /// <summary>
        /// Creates a new amenity in the database and returns the corresponding AmenityDTO.
        /// </summary>
        /// <param name="amenities">The Amenity object to be created.</param>
        /// <returns>An ActionResult containing the AmenityDTO representing the created amenity.</returns>
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenities(Amenity amenities)
        {
            return await _amenity.Create(amenities);
        }

        /// <summary>
        /// Deletes an amenity from the database by the given amenity ID and returns an IActionResult indicating the success or failure of the delete operation.
        /// </summary>
        /// <param name="id">The ID of the amenity to be deleted.</param>
        /// <returns>An IActionResult indicating the success or failure of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            return Ok(await _amenity.Delete(id));
        }


    }
}
