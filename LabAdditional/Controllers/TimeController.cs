using Microsoft.AspNetCore.Mvc;

namespace LabAdditional.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeController : ControllerBase
{
    [HttpGet]
    public ActionResult<DateTime> Get()
        => DateTime.Now;
}