using Microsoft.EntityFrameworkCore;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Infrastructure.DBContext;

namespace WebApp.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public MembershipRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Membership> CreateMembershipAsync(Membership membership)
        {
            _applicationDBContext.Memberships.Add(membership);
            await _applicationDBContext.SaveChangesAsync();
            return membership;

        }

        public Task<string> DeleteMembershipAsync(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<MembershipPlan>> GetMembershipPlansAysnc()
        {
            return await _applicationDBContext.MembershipPlans.ToListAsync();
        }

        public async Task<IList<Membership>> GetMembershipsByMemberIdAsync(string memberID)
        {
            await _applicationDBContext.Database.ExecuteSqlRawAsync("EXEC MarkInactiveMemberships");
            return await _applicationDBContext.Memberships
                                                .Include(x => x.Member)
                                                .Where(x => x.Member.MemberLoginName == memberID)
                                                .ToListAsync();

        }
        public async Task<Membership> GetMembershipByIdAsync(int id)
        {
            var membership = await _applicationDBContext.Memberships
                                                .Include(x => x.Member)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            if (membership == null)
                return null;
            return membership;
        }

        public async Task<int> UpdateMembershipAsync(Membership membership)
        {
            Membership? membershipReturn = await _applicationDBContext.Memberships.FindAsync(membership.Id);
            membershipReturn.MembershipType = membership.MembershipType;
            membershipReturn.MembershipStartDate = membership.MembershipStartDate;
            membershipReturn.MembershipEndDate = membership.MembershipEndDate;
            membershipReturn.Member = membership.Member;
            membershipReturn.MembershipAmount = membership.MembershipAmount;
            membershipReturn.PaidAmount = membership.PaidAmount;
            membershipReturn.DueAmount = membership.DueAmount;
            membershipReturn.IsMembershipActive = membership.IsMembershipActive;
            int status = await _applicationDBContext.SaveChangesAsync();
            return status;
        }
    }
}
