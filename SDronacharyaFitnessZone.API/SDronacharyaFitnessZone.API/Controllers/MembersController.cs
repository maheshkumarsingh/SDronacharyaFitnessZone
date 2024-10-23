using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Infrastructure.DBContext;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public MembersController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllUser()
        {
            var members = await _dbContext.Members.ToListAsync();
            return members;
        }
        [HttpGet("{memberId:string}")]
        public async Task<ActionResult<Member>> GetMemberById(string memberId)
        {
            var member = await _dbContext.Members.FindAsync(memberId);
            if(member == null)
                return NotFound();
            return member;
        }
    }
}
