using WebApp.Core.Domain.Entities;
using WebApp.Core.Helpers;

namespace WebApp.Core.Domain.RepositoryContracts
{
    public interface IMemberRepository
    {
        public Task<Member> GetMemberByIdAsync(string memberID);
        public Task<PagedList<Member>> GetAllMembersAsync(UsersParams usersParams);
        public Task<Member> CreateMemberAsync(Member member);
        public Task<int> UpdateMemberAsync(Member member);
        public Task<string> DeleteMemberByIdAsync(string memberID);
        public Task<Member> RegisterMember(Member member);
        public Task<Member> AuthenticateMemberAsync(string memberLoginName, string passWord);
        public Task<Member> AddMemberPhotoAsync(Member member, Photo photo);
        public Task<bool> SetMemberMainPhotoAsync(Photo photo);
        public Task<bool> DeleteMemberPhotoAsync(Member member , Photo photo);
    }
}
