using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert)
        {
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                BookingResponse booking = _repository.Add(bookingInsert, userEmail);

                return Created("booking", booking);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid)
        {
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                var userEmail = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                BookingResponse booking = _repository.GetBooking(Bookingid, userEmail);

                if (booking == null)
                {
                    return Unauthorized();
                }

                return Ok(booking);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}