using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipService _membershipService;

        public MemberService(IMemberRepository memberRepository, IMembershipRepository membershipRepository, IMembershipService membershipService)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _membershipService = membershipService;
        }

        public async Task<MemberResponseDTO> CreateMember(AddMemberRequestDTO memberAddRequestDTO)
        {
            Member member = memberAddRequestDTO.ToMember();
            Member memberReturn = await _memberRepository.CreateMember(member);
            MemberResponseDTO memberResponseDTO = memberReturn.ToMemberResponseDTO();
            AddMembershipRequestDTO addMembershipRequestDTO = new AddMembershipRequestDTO()
            {
                MemberLoginName = memberResponseDTO.MemberLoginName,
                MembershipType = memberResponseDTO.MembershipType,
                IsOldMember = memberResponseDTO.IsOldmember,
            };
            MembershipResponseDTO membershipResponseDTO =  await _membershipService.CreateMembership(addMembershipRequestDTO);
            memberResponseDTO.Memberships.Add(membershipResponseDTO);
            //Supplement code
            return memberResponseDTO;
        }

        public Task<string> DeleteMember(string memberID)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<MemberResponseDTO>> GetAllMembers()
        {
            var members = await _memberRepository.GetAllMembers();
            IList<MemberResponseDTO> memberResponseDTOs = new List<MemberResponseDTO>();
            foreach (var member in members)
            {
                MemberResponseDTO memberResponseDTO = member.ToMemberResponseDTO();
                IList<Membership> memberships = await _membershipRepository.GetMemberMembershipsList(member.MemberLoginName);
                IList<MembershipResponseDTO> membershipResponseDTOs = new List<MembershipResponseDTO>();
                foreach (Membership membership in memberships)
                {
                    membershipResponseDTOs.Add(membership.ToMembershipReponseDTO());
                }
                memberResponseDTO.Memberships = membershipResponseDTOs;
                memberResponseDTOs.Add(memberResponseDTO);
            }
            return memberResponseDTOs;
        }

        public Task<MemberResponseDTO> GetMemberById(string memberID)
        {
            throw new NotImplementedException();
        }

        public Task<MemberResponseDTO> UpdateMember(UpdateMemberRequestDTO memberUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
