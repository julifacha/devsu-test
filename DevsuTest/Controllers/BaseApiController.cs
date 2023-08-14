using Microsoft.AspNetCore.Mvc;

namespace DevsuTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected BaseApiController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
