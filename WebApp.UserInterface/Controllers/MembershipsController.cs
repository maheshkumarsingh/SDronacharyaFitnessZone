using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using WebApp.Core.Domain.Entities;
using WebApp.Core.DTOs;

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
    }
}
