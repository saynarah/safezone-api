using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using safezone.application.DTOs.Occurence;
using safezone.application.Interfaces;
using safezone.domain.Entities;
using safezone.infrastructure.Repositories;

namespace safezone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OccurenceController : ControllerBase
    {
        private readonly IOccurenceRepository _occurenceRepository;

        public OccurenceController(IOccurenceRepository occurenceRepository)
        {
            _occurenceRepository = occurenceRepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Occurrence>> GetById(int id)
        {
            var occurence = await _occurenceRepository.GetOccurrenceByIdAsync(id);

            if (occurence == null) return NotFound($"User with ID {id} not found.");
            return Ok(occurence);

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OccurenceDTO dto)
        {
            var occurence = new Occurrence
            {
                Title = dto.Title,
                Description = dto.Description,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Type = dto.Type,    
                // UserId = dto.UserId //fazer depois GEORGE
            };


            if (occurence == null) return BadRequest("Occurrence cannot be null.");
            await _occurenceRepository.AddOccurrenceAsync(occurence);
            return CreatedAtAction(nameof(GetById), new { id = occurence.Id }, occurence);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]OccurenceDTO dto)
        {
            var occurence = await _occurenceRepository.GetOccurrenceByIdAsync(id);
            if (occurence == null) return NotFound($"Occurrence with ID {id} not found.");

            occurence.Title = dto.Title;
            occurence.Description = dto.Description;
            occurence.Latitude = dto.Latitude;
            occurence.Longitude = dto.Longitude;    
            occurence.Type = dto.Type;  

            await _occurenceRepository.UpdateOccurrenceAsync(occurence);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var occurence = await _occurenceRepository.GetOccurrenceByIdAsync(id);
            if (occurence == null) return NotFound($"Occurrence with ID {id} not found.");
            await _occurenceRepository.DeleteOccurrenceAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Occurrence>>> GetAll()
        {
            var occurrences = await _occurenceRepository.GetAllOccurrencesAsync();
            return Ok(occurrences);
        }
    }
}
