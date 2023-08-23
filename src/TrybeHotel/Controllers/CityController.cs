using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Dto;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            try
            {
                IEnumerable<CityDto> cities = _repository.GetCities();

                return Ok(cities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult PostCity([FromBody] City city)
        {
            try
            {
                CityDto createdCity = _repository.AddCity(city);

                return Created("city", createdCity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
        }

        // 3. Desenvolva o endpoint PUT /city
        [HttpPut]
        public IActionResult PutCity([FromBody] City city)
        {
            try
            {
                CityDto updatedCity = _repository.UpdateCity(city);

                return Ok(updatedCity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}