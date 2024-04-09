using Microsoft.AspNetCore.Mvc;

namespace SynetraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConnectionController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}