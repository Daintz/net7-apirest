using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiRest.Models.DTO;
using ApiRest.Data;
using Microsoft.AspNetCore.JsonPatch;
using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db) {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> getVillas() {
            _logger.LogInformation("Get all Villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name="getVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> getVilla(int id) {
            if(id == 0) {
                _logger.LogError("Error to fetch villa with id" + id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> postVilla([FromBody] VillaDTO villaDTO) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if(_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("NameExists", "Villa with this name already exists");
                return BadRequest(ModelState);
            }

            if(villaDTO == null) {
                return BadRequest();
            }

            if(villaDTO.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new()
            {
                Name = villaDTO.Name
            };

            _db.Villas.Add(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("getVilla", new {id = villaDTO.Id}, villaDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> deleteVilla(int id) {
            if(id == 0) {
                return BadRequest();
            }

            var villa = await _db.Villas.FindAsync(id);
            if(villa == null) {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updateVilla(int id,[FromBody] VillaDTO villaDTO) {
            if(villaDTO == null || id != villaDTO.Id) {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name
            };

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("getVilla", new {id = villaDTO.Id}, villaDTO);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) {
            if(patchDTO == null || id == 0) {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name
            };

            patchDTO.ApplyTo(villaDTO, ModelState);


            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name
            };

            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}