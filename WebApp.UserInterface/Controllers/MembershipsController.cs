using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.ServiceContracts;
using WebApp.Core.Domain.Entities;
using WebApp.Core.DTOs;
using WebApp.UserInterface.Controllers;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    public class MembershipsController : BaseAPIController
    {
        private readonly IMembershipService _membershipService;

        public MembershipsController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet("allMemberships")]
        public async Task<IList<MembershipPlan>> GetMembershipPlans()
        {
            return await _membershipService.GetMembershipPlans();
        }
        [HttpPost]
        public async Task<ActionResult<MembershipResponseDTO>> CreateMembership(AddMembershipRequestDTO addMembership)
        {
            return await _membershipService.CreateMembership(addMembership);
        }
        [HttpGet]
        public async Task<ActionResult<IList<MembershipResponseDTO>>> GetMemberMemberships(string memberLoginId)
        {
            return Ok(await _membershipService.GetMemberMembershipsList(memberLoginId));
        }
        [HttpPut]
        public async Task<ActionResult<MembershipResponseDTO>> UpdateMembership(UpdateMembershipRequestDTO update)
        {
            var membershipResponseDTO = await _membershipService.GetMembershipById(update.Id);
            if (membershipResponseDTO != null)
            {
                membershipResponseDTO = await _membershipService.UpdateMembership(update);
                if (membershipResponseDTO != null)
                    return Ok(membershipResponseDTO);
            }
            return BadRequest("Membership not updated");
        }
    }
}
