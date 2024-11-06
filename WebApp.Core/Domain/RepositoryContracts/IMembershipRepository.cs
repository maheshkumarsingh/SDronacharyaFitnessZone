

using WebApp.Core.Domain.Entities;

namespace SDronacharyaFitnessZone.Core.Domain.RepositoryContracts
{
    public interface IMembershipRepository
    {
        public Task<IList<MembershipPlan>> GetMembershipPlans();
        public Task<Membership> CreateMembership(Membership membership);
        public Task<string> DeleteMembership(string MemberId, int MembershipId);
        public Task<IList<Membership>> GetMemberMembershipsList(string memberID);
    }
}
