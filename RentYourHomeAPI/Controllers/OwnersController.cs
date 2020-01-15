using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentYourHomeAPI.Models;
using RentYourHomeAPI.Repositories;

namespace RentYourHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _repository;

        public OwnersController(IOwnerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            try
            {
                var owners = await _repository.GetOwners();
                return Ok(owners);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            try
            {
                var owner = await _repository.GetOwner(id);

                if (owner == null)
                {
                    return NotFound();
                }

                return Ok(owner);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] Owner owner)
        {
            try
            {
                if (owner == null)
                { 
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.RegisterOwner(owner);
                await _repository.SaveAsync();

                return CreatedAtAction("GetOwner", new { id = owner.Id}, owner);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody]Owner owner)
        {
            try
            {
                if (owner == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = await _repository.GetOwner(id); 
                if (ownerEntity == null)
                {
                    return NotFound();
                }

                ownerEntity.Value.Id = id;
                ownerEntity.Value.Name = owner.Name;
                ownerEntity.Value.Password = owner.Password;

                _repository.UpdateOwner(ownerEntity.Value);
                await _repository.SaveAsync();

                return CreatedAtAction("GetOwner", new { id = ownerEntity.Value.Id }, ownerEntity);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            try
            {
                var ownerEntity = await _repository.GetOwner(id);
                if (ownerEntity == null)
                {
                    return NotFound();
                }

                _repository.DeleteOwner(ownerEntity.Value);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}