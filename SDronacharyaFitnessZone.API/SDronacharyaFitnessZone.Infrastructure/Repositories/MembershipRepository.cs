using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _applicationDBContext.Memberships
                            .Where(m => m.Member.MemberLoginName == membership.Member.MemberLoginName)
                            .OrderBy(membership => membership.MembershipStartDate)
                            .FirstOrDefaultAsync();
                            
        }

        public Task<string> DeleteMembership(string MemberId, int MembershipId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, string>> GetAllMembershipTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Membership>> GetMemberMembershipsList(string memberID)
        {
            return await _applicationDBContext.Memberships.Where(x => x.Member.MemberLoginName == memberID).ToListAsync();
        }
    }
}
