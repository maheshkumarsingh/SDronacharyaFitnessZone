using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;
using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.ServiceContracts
{
    public interface IMembershipService
    {
        public Task<Dictionary<int, string>> GetAllMembershipTypes();
        public Task<MembershipResponseDTO> CreateMembership(AddMembershipRequestDTO addMembershipRequestDTO);
        public Task<IList<MembershipResponseDTO>> GetMemberMembershipsList();
        public Task<string> DeleteMembership(string MemberId, int MembershipId);

    }
}
