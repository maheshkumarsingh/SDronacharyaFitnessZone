using WebApp.Core.Domain.Entities;

namespace WebApp.Core.Domain.RepositoryContracts
{
    public interface IMembershipRepository
    {
        public Task<IList<MembershipPlan>> GetMembershipPlansAysnc();
        public Task<Membership> CreateMembershipAsync(Membership membership);
        public Task<string> DeleteMembershipAsync(string MemberId, int MembershipId);
        public Task<IList<Membership>> GetMembershipsByMemberIdAsync(string memberID);
        public Task<Membership> GetMembershipByIdAsync(int id);
        public Task<int> UpdateMembershipAsync(Membership membership);
    }
}
