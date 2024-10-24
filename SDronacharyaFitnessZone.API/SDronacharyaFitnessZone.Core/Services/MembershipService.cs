using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMemberRepository _memberRepository;

        public MembershipService(IMembershipRepository membershipRepository, IMemberRepository memberRepository)
        {
            _membershipRepository = membershipRepository;
            _memberRepository = memberRepository;
        }

        public async Task<MembershipResponseDTO> CreateMembership(AddMembershipRequestDTO addMembershipRequestDTO)
        {
            Membership membership = new Membership()
            {
                MembershipStartDate = DateOnly.FromDateTime(DateTime.Now),
                IsMembershipActive = true,
                Member = await _memberRepository.GetMemberById(addMembershipRequestDTO.MemberLoginName),
                MembershipType = addMembershipRequestDTO.MembershipType,
            };
            return (await _membershipRepository.CreateMembership(membership)).ToMembershipReponseDTO();
        }

        public Task<string> DeleteMembership(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, string>> GetAllMembershipTypes()
        {
            throw new NotImplementedException();
        }

        public Task<IList<MembershipResponseDTO>> GetMemberMembershipsList()
        {
            throw new NotImplementedException();
        }
    }
}
