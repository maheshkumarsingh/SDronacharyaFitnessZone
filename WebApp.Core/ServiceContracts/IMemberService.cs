using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;

namespace WebApp.Core.ServiceContracts
{
    public interface IMemberService
    {
        public Task<MemberResponseDTO> GetMemberById(string memberID);
        public Task<IList<MemberResponseDTO>> GetAllMembers();
        public Task<MemberResponseDTO> CreateMember(AddMemberRequestDTO memberAddRequestDTO);
        public Task<MemberResponseDTO> UpdateMember(UpdateMemberRequestDTO memberUpdateRequestDTO);
        public Task<string> DeleteMember(string memberID);
        //public Task<MemberResponseDTO> RegisterMember(RegisterMemberRequestDTO memberRequestDTO);
        public Task<MemberResponseDTO> LoginMember(LoginMemberDTO memberRequestDTO);
        public Task<bool> SetMainPhotoForMember(int photoId, MemberResponseDTO memberResponseDTO);
        public Task<bool> DeleteMemberPhoto(MemberResponseDTO memberResponseDTO, int photoId);
    }
}
