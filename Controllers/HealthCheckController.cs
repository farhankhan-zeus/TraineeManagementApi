using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class HealthCheckController : ControllerBase
{
    [HttpGet(Name="health")]
     public IActionResult GetHealth()
     {
    
        return Ok (new {
        status= "running",
        application = "Training Management Api",
        Date = DateOnly.FromDateTime(DateTime.Now)

        });
     }
}