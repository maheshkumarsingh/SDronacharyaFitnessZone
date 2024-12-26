using WebApp.Core.DTOs;
using WebApp.Core.Domain.Entities;

namespace WebApp.Core.ServiceContracts
{
    public interface IMembershipService
    {
        public Task<IList<MembershipPlan>> GetMembershipPlans();
        public Task<MembershipResponseDTO> CreateMembership(AddMembershipRequestDTO addMembershipRequestDTO);
        public Task<IList<MembershipResponseDTO>> GetMemberMembershipsList(string memberLoginId);
        public Task<string> DeleteMembership(string MemberId, int MembershipId);
        public Task<MembershipResponseDTO> GetMembershipById(int id);
        public Task<MembershipResponseDTO> UpdateMembership(UpdateMembershipRequestDTO updateMembershipRequestDTO);

    }
}
