using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.Domain.RepositoryContracts
{
    public interface IMembershipRepository
    {
        public Task<Dictionary<int, string>> GetAllMembershipTypes();
        public Task<Membership> CreateMembership(Membership membership);
        public Task<string> DeleteMembership(string MemberId, int MembershipId);
        public Task<IList<Membership>> GetMemberMembershipsList(string memberID);
    }
}
