using Microsoft.AspNetCore.Mvc;

namespace ContanerizedVisits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        [HttpGet]
        public string GetVersion()
        {
            return $"Hola!! soy una versión contenerizada en Kubernetes";
        }
    }

   
}