using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace todolist_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoitemController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetUserTodoitems()
        {
            return Ok("Jesteś autoryzowany do tej treści.");
        }
    }
}