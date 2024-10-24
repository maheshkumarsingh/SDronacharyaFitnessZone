using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.DTOs
{
    public class MemberResponseDTO
    {
        public int Id { get; set; }
        public string MemberLoginName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public MembershipType  MembershipType { get; set; }
        public DateOnly JoiningDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public bool IsOldmember { get; set; }
        public string ImageUrl { get; set; }
        public IList<MembershipResponseDTO> Memberships { get; set; }
        public IList<SupplementOrder> SupplementOrders { get; set; }
    }

    public static class MemberExtension
    {
        public static MemberResponseDTO ToMemberResponseDTO(this Member member)
        {
            return new MemberResponseDTO()
            {
                Id = member.Id,
                MemberLoginName = member.MemberLoginName,
                FirstName = member.FirstName,
                MiddleName = member.MiddleName,
                LastName = member.LastName,
                DateOfBirth = member.DateOfBirth,
                Age = member.Age,
                Gender = member.Gender,
                Email = member.Email,
                Password = member.Password,
                PhoneNumber = member.PhoneNumber,
                AlternatePhoneNumber = member.AlternatePhoneNumber,
                Address = member.Address,
                BloodGroup = member.BloodGroup,
                MembershipType = member.MembershipType,
                JoiningDate = member.JoiningDate,
                IsOldmember = member.IsOldMember,
                ImageUrl = member.ImageUrl
            };
        }
    }
}
