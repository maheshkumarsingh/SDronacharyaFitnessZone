using WebApp.Core.DTOs;
using WebApp.Core.Helpers;

namespace WebApp.Core.ServiceContracts
{
    public interface IMemberService
    {
        public Task<MemberResponseDTO> GetMemberByIdAsync(string memberID);
        public Task<PagedList<MemberResponseDTO>> GetAllMembersAsync(UsersParams usersParams);
        public Task<MemberResponseDTO> CreateMemberAsync(AddMemberRequestDTO memberAddRequestDTO);
        public Task<int> UpdateMemberAsync(UpdateMemberRequestDTO memberUpdateRequestDTO);
        public Task<string> DeleteMemberAsync(string memberID);
        //public Task<MemberResponseDTO> RegisterMember(RegisterMemberRequestDTO memberRequestDTO);
        public Task<MemberResponseDTO> AuthenticateMemberAsync(LoginMemberDTO memberRequestDTO);
        public Task<bool> SetMemberMainPhotoAsync(int photoId, MemberResponseDTO memberResponseDTO);
        public Task<bool> DeleteMemberPhotoAsync(MemberResponseDTO memberResponseDTO, int photoId);
    }
}
