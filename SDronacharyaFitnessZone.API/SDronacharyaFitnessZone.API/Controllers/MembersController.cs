using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using SDronacharyaFitnessZone.Infrastructure.DBContext;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberservice)
        {
            _memberService = memberservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }
        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMemberById(string memberId)
        {
            var member = await _memberService.GetMemberById(memberId);
            if(member == null)
                return NotFound();
            return Ok(member);
        }
        [HttpPost]
        public async Task<ActionResult<MemberResponseDTO>> CreateMember(AddMemberRequestDTO requestDTO)
        {
            MemberResponseDTO memberResponseDTO = await _memberService.CreateMember(requestDTO);
            return Ok(memberResponseDTO);
        }
    }
}
