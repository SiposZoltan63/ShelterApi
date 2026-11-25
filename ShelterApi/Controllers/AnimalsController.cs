using ShelterApi.Models;
using ShelterApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ShelterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalShelterContext _context;

        public AnimalsController(AnimalShelterContext context)
        {
            _context = context;
        }

        [HttpPost("NewAnimal")]
        public async Task<IActionResult> NewAnimal([FromBody] NewAnimalsDTO NewAnimalsDto)
        {
            var animals = new Animals
            {
                Id = Guid.NewGuid(),
                Name = NewAnimalsDto.Name,
                Species = NewAnimalsDto.Species,
                Age = NewAnimalsDto.Age,
                Weight = NewAnimalsDto.Weight,
                RegTime = DateTime.Now,
                ShelterId = NewAnimalsDto.ShleterID,
            };
            if (animals is not null)
            {
                await _context.Animals.AddAsync(animals);
                await _context.SaveChangesAsync();
                return StatusCode(201, new { message = "Felvéve", result = animals });
            }
            return StatusCode(400, new { message = "Hiba", result = animals });
        }
        [HttpGet("AllAnimal")]
        public async Task<IActionResult> AllAnimal()
        {
            var animals = await _context.Animals.ToListAsync();
            if (animals is not null)
            {
                return StatusCode(201, new { message = "Lekérdezve", result = animals });
            }
            return StatusCode(400, new { message = "Hiba", result = animals });
        }
        [HttpGet("AnimalById")]
        public async Task<IActionResult> AnimalById([FromQuery] Guid id)
        {
            var animals = await _context.Animals.SingleOrDefaultAsync(r => r.Id == id);
            if (animals is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve" });
            }
            return StatusCode(400, new { message = "Nincs találat", result = animals });
        }
        [HttpPut("UpdateAnimal")]
        public async Task<IActionResult> UpdateAnimal([FromQuery] Guid id, [FromBody] UpdateAnimalsDTO updateAnimalsDto)
        {
            var animals = await _context.Animals.FirstOrDefaultAsync(r => r.Id == id);
            if (animals is not null)
            {
                animals.Name = updateAnimalsDto.Name;
                _context.Animals.Update(animals);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { message = "Frissítve" });
            }
            return StatusCode(400, new { message = "Nincs találat", result = animals });
        }
        [HttpDelete("DeleteAnimal")]
        public async Task<IActionResult> DeleteAnimal([FromQuery] Guid id)
        {
            var animals = await _context.Animals.FindAsync(id);
            if (animals is not null)
            {
                _context.Animals.Remove(animals);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { message = "Törölve" });
            }
            return StatusCode(400, new { message = "Sikertelen törlés", result = animals });
        }
    }
}
