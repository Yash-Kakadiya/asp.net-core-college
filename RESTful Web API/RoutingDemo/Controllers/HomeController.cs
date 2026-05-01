using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace RoutingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("yash/{id:int}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetV1(int id)
        {
            return Ok($"v1 Hello from HomeController!\nid:{id}");
        }

        // same method with different logic but same route with different versioning
        [HttpGet]
        [Route("yash/{id:int}")]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2(int id)
        {
            return Ok($"v2 Hello from HomeController!\nid:{id}");
        }
    }
}
