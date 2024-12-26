using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.DTOs;

namespace SDronacharyaFitnessZone.Core.ServiceContracts
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
