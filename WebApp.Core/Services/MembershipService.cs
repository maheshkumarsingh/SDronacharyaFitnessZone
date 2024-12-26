using WebApp.Core.DTOs;
using WebApp.Core.ServiceContracts;
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
                Member = await _memberRepository.GetMemberByIdAsync(addMembershipRequestDTO.MemberLoginName),
                PaidAmount = addMembershipRequestDTO.PaidAmount,
            };
            return (await _membershipRepository.CreateMembershipAsync(membership)).ToMembershipReponseDTO();
        }

        public Task<string> DeleteMembership(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MembershipPlan>> GetMembershipPlans()
        {
            return _membershipRepository.GetMembershipPlansAysnc();
        }

        public async Task<IList<MembershipResponseDTO>> GetMemberMembershipsList(string memberLoginId)
        {
            var memberships = await _membershipRepository.GetMembershipsByMemberIdAsync(memberLoginId);
            return memberships.Select(x => x.ToMembershipReponseDTO()).ToList();
        }
        public async Task<MembershipResponseDTO> GetMembershipById(int id)
        {
            var membership = await _membershipRepository.GetMembershipByIdAsync(id);
            return membership.ToMembershipReponseDTO();
        }
        public async Task<MembershipResponseDTO> UpdateMembership(UpdateMembershipRequestDTO updateMembershipRequestDTO)
        {
            Membership membership = new Membership()
            {
                Id = updateMembershipRequestDTO.Id,
                MembershipType = updateMembershipRequestDTO.MembershipType,
                MembershipStartDate = updateMembershipRequestDTO.MembershipStartDate,
                Member = await _memberRepository.GetMemberByIdAsync(updateMembershipRequestDTO.MemberLoginName),
                PaidAmount = updateMembershipRequestDTO.PaidAmount,
            };
            if(await _membershipRepository.UpdateMembershipAsync(membership) > 0)
                 return membership.ToMembershipReponseDTO();
            return null;
        }
    }
}
