using Microsoft.AspNetCore.Mvc;
using PetShop.DeveloperTesting._02.ServiceLayer.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.DeveloperTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetCategoriesController : ControllerBase
    {
        private readonly IPetCategoriesService _petCategoriesService;

        public PetCategoriesController(IPetCategoriesService petCategoriesService)
        {
            _petCategoriesService=petCategoriesService;
        }


        // GET: api/<PetCategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            var allCategories = _petCategoriesService.GetCategories();
            return Ok(allCategories);
        }

        // GET api/<PetCategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
    }
}
