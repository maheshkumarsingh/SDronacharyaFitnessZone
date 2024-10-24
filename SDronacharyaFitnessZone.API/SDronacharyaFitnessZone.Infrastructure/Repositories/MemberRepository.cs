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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public MemberRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member> CreateMember(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberLoginName == member.MemberLoginName);
        }

        public Task<string> DeleteMember(string memberID)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Member>> GetAllMembers()
        {
            return await _dbContext.Members.ToListAsync();
        }

        public async Task<Member> GetMemberById(string memberLoginName)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberLoginName == memberLoginName);
        }

        public Task<Member> UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
