using Microsoft.AspNetCore.Mvc;
using PetShop.DeveloperTesting._02.ServiceLayer.Exceptions;
using PetShop.DeveloperTesting.Business.Entities;
using PetShop.DeveloperTesting.ServiceLayer.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetStoreController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetStoreController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allPets = _petService.GetAll();
            return Ok(allPets);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PetDto newPet)
        {
            try
            {
                var petId = _petService.IncreasePetSupply(newPet);
                return Ok(petId);
            }
            catch (ConflictException ex)
            {
                return Conflict();
            }
            catch (OverloadException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT api/<AnimalStoreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PetDto updatePet)
        {
            try
            {
                _petService.SellPet(updatePet);
                return Ok();
            }
            catch (OutOfStockException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
