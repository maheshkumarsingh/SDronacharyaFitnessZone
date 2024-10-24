using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.ServiceContracts
{
    public interface IMemberService
    {
        public Task<MemberResponseDTO> GetMemberById(string memberID);
        public Task<IList<MemberResponseDTO>> GetAllMembers();
        public Task<MemberResponseDTO> CreateMember(AddMemberRequestDTO memberAddRequestDTO);
        public Task<MemberResponseDTO> UpdateMember(UpdateMemberRequestDTO memberUpdateRequestDTO);
        public Task<string> DeleteMember(string memberID);
    }
}
