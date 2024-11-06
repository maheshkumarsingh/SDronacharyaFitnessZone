using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using WebApp.Core.ServiceContracts;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    public class MembersController : BaseAPIController
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberservice)
        {
            _memberService = memberservice;
        }

        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }
        [Authorize]
        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMemberById(string memberId)
        {
            var member = await _memberService.GetMemberById(memberId);
            if(member == null)
                return NotFound();
            return Ok(member);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MemberResponseDTO>> CreateMember(AddMemberRequestDTO requestDTO)
        {
            MemberResponseDTO memberResponseDTO = await _memberService.CreateMember(requestDTO);
            return Ok(memberResponseDTO);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteMember(string memberLoginName)
        {
            return Ok(true);
        }
    }
}
