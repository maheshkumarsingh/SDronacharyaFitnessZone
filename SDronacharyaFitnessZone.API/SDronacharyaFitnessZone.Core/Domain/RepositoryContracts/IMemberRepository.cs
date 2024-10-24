using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.Domain.RepositoryContracts
{
    public interface IMemberRepository
    {
        public Task<Member> GetMemberById(string memberID);
        public Task<IList<Member>> GetAllMembers();
        public Task<Member> CreateMember(Member member);
        public Task<Member> UpdateMember(Member member);
        public Task<string> DeleteMember(string memberID);
    }
}
