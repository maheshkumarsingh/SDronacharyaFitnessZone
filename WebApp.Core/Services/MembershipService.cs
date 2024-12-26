using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Core.DTOs;

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
                MembershipType = addMembershipRequestDTO.MembershipType,
                MembershipStartDate = addMembershipRequestDTO.MembershipStartDate,
                Member = await _memberRepository.GetMemberById(addMembershipRequestDTO.MemberLoginName),
                PaidAmount = addMembershipRequestDTO.PaidAmount,
            };
            return (await _membershipRepository.CreateMembership(membership)).ToMembershipReponseDTO();
        }

        public Task<string> DeleteMembership(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MembershipPlan>> GetMembershipPlans()
        {
            return _membershipRepository.GetMembershipPlans();
        }

        public async Task<IList<MembershipResponseDTO>> GetMemberMembershipsList(string memberLoginId)
        {
            var memberships = await _membershipRepository.GetMemberMembershipsList(memberLoginId);
            return memberships.Select(x => x.ToMembershipReponseDTO()).ToList();
        }
        public async Task<MembershipResponseDTO> GetMembershipById(int id)
        {
            var membership = await _membershipRepository.GetMembershipById(id);
            return membership.ToMembershipReponseDTO();
        }
        public async Task<MembershipResponseDTO> UpdateMembership(UpdateMembershipRequestDTO updateMembershipRequestDTO)
        {
            Membership membership = new Membership()
            {
                Id = updateMembershipRequestDTO.Id,
                MembershipType = updateMembershipRequestDTO.MembershipType,
                MembershipStartDate = updateMembershipRequestDTO.MembershipStartDate,
                Member = await _memberRepository.GetMemberById(updateMembershipRequestDTO.MemberLoginName),
                PaidAmount = updateMembershipRequestDTO.PaidAmount,
            };
            if(await _membershipRepository.UpdateMembership(membership) > 0)
                 return membership.ToMembershipReponseDTO();
            return null;
        }
    }
}
