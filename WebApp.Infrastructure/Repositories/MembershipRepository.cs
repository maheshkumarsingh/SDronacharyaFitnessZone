using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using WebApp.Core.Domain.Entities;
using WebApp.Infrastructure.DBContext;

namespace SDronacharyaFitnessZone.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public MembershipRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            _applicationDBContext.Memberships.Add(membership);
            await _applicationDBContext.SaveChangesAsync();
            return membership;
                            
        }

        public Task<string> DeleteMembership(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<MembershipPlan>> GetMembershipPlans()
        {
            return await _applicationDBContext.MembershipPlans.ToListAsync();
        }

        public async Task<IList<Membership>> GetMemberMembershipsList(string memberID)
        {
            await _applicationDBContext.Database.ExecuteSqlRawAsync("EXEC MarkInactiveMemberships");
            return await _applicationDBContext.Memberships
                                                .Include(x => x.Member)
                                                .Where(x => x.Member.MemberLoginName == memberID)
                                                .ToListAsync();

        }
        public async Task<Membership> GetMembershipById(int id)
        {
            var membership = await _applicationDBContext.Memberships
                                                .Include(x => x.Member)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            if (membership == null)
                return null;
            return membership;
        }

        public async Task<int> UpdateMembership(Membership membership)
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
