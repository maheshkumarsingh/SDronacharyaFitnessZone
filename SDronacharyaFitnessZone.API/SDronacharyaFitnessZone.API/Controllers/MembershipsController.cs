using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipsController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipsController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetAllMembershipTypes()
        {
            return null;
        }
        [HttpPost]
        public async Task<ActionResult<MembershipResponseDTO>> CreateMembership(AddMembershipRequestDTO addMembership)
        {
            return await _membershipService.CreateMembership(addMembership);
        }
    }
}
