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
                IsMembershipActive = true,
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
    }
}
