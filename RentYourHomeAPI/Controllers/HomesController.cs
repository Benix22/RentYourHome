using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentYourHomeAPI.Helpers;
using RentYourHomeAPI.Models;
using RentYourHomeAPI.Repositories;

namespace RentYourHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomesController : ControllerBase
    {
        private readonly IHomeRepository _repository;

        public HomesController(IHomeRepository repository)
        {
            _repository = repository;
        }

        //// GET: api/1/HomesByOwner
        [HttpGet("{id}/HomesByOwner")]
        public async Task<ActionResult<IEnumerable<Owner>>> GetHomesByOwner(int id, [FromQuery] PagingParameters pagingParameters)
        {
            try
            {
                var homes = await _repository.GetHomesByOwner(id, pagingParameters);
                if (homes == null)
                {
                    return NotFound();
                }

                var metadata = new
                {
                    homes.Value.TotalCount,
                    homes.Value.PageSize,
                    homes.Value.CurrentPage,
                    homes.Value.TotalPages,
                    homes.Value.HasNext,
                    homes.Value.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(homes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: api/Homes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetHome(int id)
        {
            try
            {
                var homes = await _repository.GetHome(id);

                if (homes == null)
                {
                    return NotFound();
                }

                return Ok(homes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] Home home)
        {
            try
            {
                if (home == null)
                { 
                    return BadRequest("Home object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.RegisterHome(home);
                await _repository.SaveAsync();

                return CreatedAtAction("GetHome", new { id = home.Id}, home);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateHome(int id, [FromBody] Home home)
        {
            try
            {
                if (home == null)
                {
                    return BadRequest("Home object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var homeEntity = await _repository.GetHome(id); 
                if (homeEntity == null)
                {
                    return NotFound();
                }

                homeEntity.Value.Id = id;
                homeEntity.Value.OwnerId = home.OwnerId;
                homeEntity.Value.Name = home.Name;
                homeEntity.Value.City = home.City;
                homeEntity.Value.Description = home.Description;
                homeEntity.Value.Stars = home.Stars;
                homeEntity.Value.Imagen = home.Imagen;

                _repository.UpdateHome(homeEntity.Value);
                await _repository.SaveAsync();

                return CreatedAtAction("GetHome", new { id = homeEntity.Value.Id }, homeEntity);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHome(int id)
        {
            try
            {
                var homeEntity = await _repository.GetHome(id);
                if (homeEntity == null)
                {
                    return NotFound();
                }

                _repository.DeleteHome(homeEntity.Value);
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