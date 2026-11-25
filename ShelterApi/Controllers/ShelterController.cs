using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Controllers;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;

namespace ShelterApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly AnimalShelterContext _context;

        public SheltersController(AnimalShelterContext context)
        {
            _context = context;
        }

        [HttpPost("newShelter")]
        public async Task<IActionResult> NewShelter([FromBody] NewSheltersDTO NewShelterDto)
        {
            var shelter = new Shelters
            {
                Id = Guid.NewGuid(),
                Name = NewShelterDto.Name,
                City = NewShelterDto.City,
                Capacity = NewShelterDto.Capacity,
                RegTime = DateTime.Now
            };
            if (shelter is not null)
            {
                await _context.Shelters.AddAsync(shelter);
                await _context.SaveChangesAsync();
                return StatusCode(201, new { message = "Felvéve", result = shelter });
            }
            return StatusCode(400, new { message = "Hiba", result = shelter });
        }
        [HttpGet("allShelter")]
        public async Task<IActionResult> AllShelter()
        {
            var shelters = await _context.Shelters.ToListAsync();
            if (shelters is not null)
            {
                return StatusCode(201, new { message = "Lekérdezve", result = shelters });
            }
            return StatusCode(400, new { message = "Hiba", result = shelters });
        }
        [HttpGet("ShelterbyId")]
        public async Task<IActionResult> ShelterById([FromQuery] Guid id)
        {
            var shelter = await _context.Shelters.SingleOrDefaultAsync(u => u.Id == id);
            if (shelter is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve" });
            }
            return StatusCode(400, new { message = "Nincs találat", result = shelter });
        }
        [HttpPut("UpdateShelter")]
        public async Task<IActionResult> UpdateShelter([FromQuery] Guid id, [FromBody] UpdateSheltersDTO updateShelterDto)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(u => u.Id == id);
            if (shelter is not null)
            {
                shelter.Name = updateShelterDto.Name;
                shelter.City = updateShelterDto.City;
                shelter.Capacity = updateShelterDto.Capacity;
                _context.Shelters.Update(shelter);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { message = "Frissítve" });
            }
            return StatusCode(400, new { message = "Nincs találat", result = shelter });
        }
        [HttpDelete("deleteShelter")]
        public async Task<IActionResult> DeleteShelter([FromQuery] Guid id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter is not null)
            {
                _context.Shelters.Remove(shelter);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { message = "Törölve" });
            }
            return StatusCode(400, new { message = "Sikertelen törlés", result = shelter });
        }

    }
}
