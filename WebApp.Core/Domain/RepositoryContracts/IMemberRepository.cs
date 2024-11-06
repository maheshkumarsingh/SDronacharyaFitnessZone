using WebApp.Core.Domain.Entities;

namespace WebApp.Core.Domain.RepositoryContracts
{
    public interface IMemberRepository
    {
        public Task<Member> GetMemberById(string memberID);
        public Task<IList<Member>> GetAllMembers();
        public Task<Member> CreateMember(Member member);
        public Task<Member> UpdateMember(Member member);
        public Task<string> DeleteMember(string memberID);
        public Task<Member> RegisterMember(Member member);
        public Task<Member> LoginMember(string memberLoginName, string passWord);
    }
}
