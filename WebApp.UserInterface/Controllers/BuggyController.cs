using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.UserInterface.Controllers;
using WebApp.Core.Domain.Entities;
using WebApp.Infrastructure.DBContext;

namespace API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly ApplicationDBContext _context;
        public BuggyController(ApplicationDBContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<Member> GetNotFound()
        {
            var thing = _context.Members.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Members.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}