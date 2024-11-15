using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Core.DTOs;
using WebApp.Core.ServiceContracts;

namespace WebApp.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipService _membershipService;
        private readonly ITokenService _tokenService;

        public MemberService(IMemberRepository memberRepository, IMembershipRepository membershipRepository, IMembershipService membershipService, ITokenService tokenService)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _membershipService = membershipService;
            _tokenService = tokenService;
        }

        public async Task<MemberResponseDTO> CreateMember(AddMemberRequestDTO memberAddRequestDTO)
        {
            Member member = memberAddRequestDTO.ToMember();
            Member memberReturn = await _memberRepository.CreateMember(member);
            MemberResponseDTO memberResponseDTO = memberReturn.ToMemberResponseDTO();
            return memberResponseDTO;
        }

        public Task<string> DeleteMember(string memberID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMemberPhoto(MemberResponseDTO memberResponseDTO, int photoId)
        {
            var member = await _memberRepository.GetMemberById(memberResponseDTO.MemberLoginName);
            var photo = member.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return false;
            var status = await _memberRepository.DeleteMemberPhoto(member, photo);
            return status;
        }

        public async Task<IList<MemberResponseDTO>> GetAllMembers()
        {
            var members = await _memberRepository.GetAllMembers();
            var memberResponseDTOs = new List<MemberResponseDTO>();
            foreach (var member in members)
            {
                var memberResponseDTO = member.ToMemberResponseDTO();
                memberResponseDTO.Memberships = member.Memberships!.Select(x => x.ToMembershipReponseDTO()).ToList();
                memberResponseDTO.Photos = member.Photos.Select(x => x.ToPhotoResponseDTO()).ToList();
                memberResponseDTO.SupplementOrders = member.SupplementOrders.Select(x => x.ToSupplementResponseDTO()).ToList();
                memberResponseDTOs.Add(memberResponseDTO);
            }
            return memberResponseDTOs;
        }

        public async Task<MemberResponseDTO> GetMemberById(string memberID)
        {
            var member = await _memberRepository.GetMemberById(memberID);
            //IList<Membership> memberships = await _membershipRepository.GetMemberMembershipsList(member.MemberLoginName);
            MemberResponseDTO memberResponseDTO = member.ToMemberResponseDTO();
            memberResponseDTO.Memberships = member.Memberships!.Select(x => x.ToMembershipReponseDTO()).ToList();
            memberResponseDTO.Photos = member.Photos.Select(x => x.ToPhotoResponseDTO()).ToList();
            memberResponseDTO.SupplementOrders = member.SupplementOrders.Select(x=>x.ToSupplementResponseDTO()).ToList();
            if (member != null)
            {
                return memberResponseDTO;
            }
            return null;
        }

        public async Task<MemberResponseDTO> LoginMember(LoginMemberDTO memberRequestDTO)
        {
            Member member = await _memberRepository.LoginMember(memberRequestDTO.MemberLoginName, memberRequestDTO.Password);
            if (member != null)
            {
                MemberResponseDTO memberResponseDTO = member.ToMemberResponseDTO();
                memberResponseDTO.Memberships = member.Memberships!.Select(x => x.ToMembershipReponseDTO()).ToList();
                memberResponseDTO.Photos = member.Photos.Select(x => x.ToPhotoResponseDTO()).ToList();
                memberResponseDTO.SupplementOrders = member.SupplementOrders.Select(x => x.ToSupplementResponseDTO()).ToList();
                memberResponseDTO.Token = _tokenService.CreateToken(member);
                return memberResponseDTO;
            }
            return null;
        }

        public async Task<bool> SetMainPhotoForMember(int photoId, MemberResponseDTO memberResponseDTO)
        {
            var member = await _memberRepository.GetMemberById(memberResponseDTO.MemberLoginName);
            if (member == null) return false;
            var photo = member.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.IsMain) return false;
            var currentMain = member.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
            {
                currentMain.IsMain = false;
            }
            photo.IsMain = true;
            return await _memberRepository.SetMainPhotoForMember(photo);
        }

        //public async Task<MemberResponseDTO> RegisterMember(RegisterMemberRequestDTO memberRequestDTO)
        //{
        //    Member member = memberRequestDTO.ToMember();
        //    Member memberReturn = await _memberRepository.RegisterMember(member);
        //    MemberResponseDTO memberResponseDTO = memberReturn.ToMemberResponseDTO();
        //    AddMembershipRequestDTO addMembershipRequestDTO = new AddMembershipRequestDTO()
        //    {
        //        MemberLoginName = memberResponseDTO.MemberLoginName,
        //        MembershipType = memberResponseDTO.MembershipType,
        //        IsOldMember = memberResponseDTO.IsOldmember,
        //    };
        //    MembershipResponseDTO membershipResponseDTO = await _membershipService.CreateMembership(addMembershipRequestDTO);
        //    memberResponseDTO.Memberships = new List<MembershipResponseDTO>();
        //    memberResponseDTO.Memberships.Add(membershipResponseDTO);
        //    //Supplement code
        //    return memberResponseDTO;
        //}

        public async Task<MemberResponseDTO> UpdateMember(UpdateMemberRequestDTO memberUpdateRequestDTO)
        {
            Member member = memberUpdateRequestDTO.ToMember();
            Member memberReturn = await _memberRepository.UpdateMember(member);
            MemberResponseDTO memberResponseDTO = memberReturn.ToMemberResponseDTO();
            return memberResponseDTO;
        }
    }
}
