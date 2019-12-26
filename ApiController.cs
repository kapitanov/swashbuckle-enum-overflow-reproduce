using Microsoft.AspNetCore.Mvc;

namespace Swashbuckle.AspNetCore.Bug
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet("~/api/flags")]
        public Flags Get() => Flags.None;
    }
}
