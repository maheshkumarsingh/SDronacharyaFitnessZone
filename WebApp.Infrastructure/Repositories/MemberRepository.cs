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
using WebApp.Core.Helpers;
using WebApp.Infrastructure.DBContext;

namespace WebApp.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public MemberRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member> AddMemberPhotoAsync(Member member, Photo photo)
        {
            //member.Photos.Add(photo);
            _dbContext.Photos.Add(photo);
            int status = await _dbContext.SaveChangesAsync();
            if (status > 0)
                return member;
            return null;
        }

        public async Task<Member> CreateMemberAsync(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Members.FirstOrDefaultAsync(x => x.MemberLoginName == member.MemberLoginName);
        }

        public Task<string> DeleteMemberByIdAsync(string memberID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMemberPhotoAsync(Member member, Photo photo)
        {
            _dbContext.Photos.Remove(photo);
            int status = await _dbContext.SaveChangesAsync();
            return status > 0;
        }

        public async Task<PagedList<Member>> GetAllMembersAsync(UsersParams usersParams)
        {
            var query = _dbContext.Members
                                    .Include(m => m.Memberships)
                                    .Include(m => m.SupplementOrders)
                                    .Include(m => m.Photos);
            return await PagedList<Member>.Create(query, usersParams.PageNumber, usersParams.PageSize);
        }

        public async Task<Member> GetMemberByIdAsync(string memberLoginName)
        {
            return await _dbContext.Members
                                            .Include(m => m.Memberships)
                                            .Include(m => m.SupplementOrders)
                                            .Include(m => m.Photos)
                                            .FirstOrDefaultAsync(m => m.MemberLoginName == memberLoginName);
        }

        public async Task<Member> AuthenticateMemberAsync(string memberLoginName, string passWord)
        {
            Member member = await GetMemberByIdAsync(memberLoginName);
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

        public async Task<bool> SetMemberMainPhotoAsync(Photo photo)
        {
            int status = await _dbContext.SaveChangesAsync();
            return status > 0;
        }

        public async Task<int> UpdateMemberAsync(Member updateMember)
        {
            Member member = await _dbContext.Members.FindAsync(updateMember.MemberLoginName);
            member.FirstName = updateMember.FirstName;
            member.MiddleName = updateMember.MiddleName;
            member.LastName = updateMember.LastName;
            member.PhoneNumber = updateMember.PhoneNumber;
            member.DateOfBirth = updateMember.DateOfBirth;
            member.Address = updateMember.Address;
            member.AlternatePhoneNumber = updateMember.AlternatePhoneNumber;
            member.BloodGroup = updateMember.BloodGroup;
            member.JoiningDate = updateMember.JoiningDate;
            int status = await _dbContext.SaveChangesAsync();
            return status;

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
