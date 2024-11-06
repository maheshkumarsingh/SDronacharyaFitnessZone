using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Infrastructure.DBContext;

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
            return await _dbContext.Members
                                    .Include(m => m.Memberships)
                                    .Include(m => m.SupplementOrders)
                                    .Include(m => m.Photos)
                                    .ToListAsync();
        }

        public async Task<Member> GetMemberById(string memberLoginName)
        {
            return await _dbContext.Members
                                            .Include(m => m.Memberships)
                                            .Include(m => m.SupplementOrders)
                                            .Include(m => m.Photos)
                                            .FirstOrDefaultAsync(m => m.MemberLoginName == memberLoginName);
        }

        public async Task<Member> LoginMember(string memberLoginName, string passWord)
        {
            Member member = await GetMemberById(memberLoginName);
            if (CheckLoginPassword(member, passWord))
            {
                return member;
            }
            return null;
        }

        public async Task<Member> RegisterMember(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberLoginName == member.MemberLoginName);
        }

        public Task<Member> UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }

        private bool CheckLoginPassword(Member member, string loginDTOPassword)
        {
            using var hmac = new HMACSHA512(member.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTOPassword));
            
            for(int i=0;i<computedHash.Length;i++)
            {
                if (computedHash[i] != member.Password[i])
                    return false;
            }
            return true;
        }
    }
}
