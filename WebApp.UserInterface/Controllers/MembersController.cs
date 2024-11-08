using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.Core.DTOs;
using WebApp.Core.ServiceContracts;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    [Authorize]
    public class MembersController : BaseAPIController
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberservice)
        {
            _memberService = memberservice;
        }
        [HttpGet()]
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
        [HttpDelete]
        public async Task<ActionResult> DeleteMember(string memberLoginName)
        {
            return Ok(true);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMember(UpdateMemberRequestDTO requestDTO)
        {
            var memberResponse = await _memberService.GetMemberById(requestDTO.MemberLoginName);
            if(memberResponse != null)
            {
                var memberResponseDTO = _memberService.UpdateMember(requestDTO);
                return Ok(memberResponseDTO);
            }
            return BadRequest();
        }
    }
}
