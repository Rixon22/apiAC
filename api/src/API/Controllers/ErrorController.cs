using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : BaseApiController
    {
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request");
        }
        [HttpGet("auth")]
        public IActionResult GetAuth()
        {
            return Unauthorized("This is an unauthorized request");
        }
        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound("This resource was not found");
        }
        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
    }

}
