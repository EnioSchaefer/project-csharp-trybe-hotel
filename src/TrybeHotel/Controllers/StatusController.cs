using Microsoft.AspNetCore.Mvc;


namespace TrybeHotel.Controllers
{
  [ApiController]
  [Route("/")]
  public class StatusController : Controller
  {
    [HttpGet]
    public object GetStatus()
    {
      try
      {
        object ReturnMessage = new { message = "online" };

        return Ok(ReturnMessage);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return BadRequest(e.Message);
      }
    }
  }
}