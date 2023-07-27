using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    /// <summary>
    /// Custom controller versioning
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {

    }
}

