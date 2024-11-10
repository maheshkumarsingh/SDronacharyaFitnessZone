using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.Entities.Enums;
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

        public async Task<Member> AddMemberPhoto(Member member, Photo photo)
        {
            member.Photos.Add(photo);
            int status = await _dbContext.SaveChangesAsync();
            if (status > 1)
                return member;
            return null;
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

        public async Task<Member> UpdateMember(Member member)
        {
            var memberMatching = await GetMemberById(member.MemberLoginName);
            if (memberMatching == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }
            //memberMatching.MemberLoginName = member.MemberLoginName;
            memberMatching.FirstName = member.FirstName;
            memberMatching.MiddleName = member.MiddleName;
            memberMatching.LastName = member.LastName;
            memberMatching.DateOfBirth = member.DateOfBirth;
            memberMatching.Gender = member.Gender;
            //memberMatching.Email = member.Email;
            memberMatching.Password = member.Password;
            memberMatching.PasswordSalt = member.PasswordSalt;
            memberMatching.PhoneNumber = member.PhoneNumber;
            memberMatching.AlternatePhoneNumber = member.AlternatePhoneNumber;
            memberMatching.Address = member.Address;
            memberMatching.BloodGroup = member.BloodGroup;
            memberMatching.JoiningDate = member.JoiningDate;
            //memberMatching.Memberships = member.Memberships;
            //memberMatching.Photos = member.Photos;
            //memberMatching.SupplementOrders = member.SupplementOrders;

            // Update member in the database
            //_dbContext.Members.Update(memberMatching);
            await _dbContext.SaveChangesAsync();

            return memberMatching;
        }
        private bool CheckLoginPassword(Member member, string loginDTOPassword)
        {
            using var hmac = new HMACSHA512(member.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTOPassword));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != member.Password[i])
                    return false;
            }
            return true;
        }
    }
}
