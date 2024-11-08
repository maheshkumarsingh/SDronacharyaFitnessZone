using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities.Enums;
using WebApp.Core.Domain.Entities;

namespace SDronacharyaFitnessZone.Core.DTOs
{
    public class MembershipResponseDTO
    {
        public int Id { get; set; }
        public MembershipType MembershipType { get; set; }
        public bool IsMembershipActive { get; set; }
        public DateOnly MembershipStartDate { get; set; }
        public DateOnly MembershipEndDate { get; set; }
        public double MembershipAmount { get; set; }
        public string MemberLoginName { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
    }

    public static class MembershipExtension
    {
        public static MembershipResponseDTO ToMembershipReponseDTO(this Membership membership)
        {
            return new MembershipResponseDTO()
            {
                Id = membership.Id,
                MembershipType = membership.MembershipType,
                IsMembershipActive = membership.IsMembershipActive,
                MembershipStartDate = membership.MembershipStartDate,
                MembershipEndDate = membership.MembershipEndDate,
                MembershipAmount = membership.MembershipAmount,
                //MemberLoginName = membership.Member.MemberLoginName,
                PaidAmount = membership.PaidAmount,
                DueAmount = membership.DueAmount,
            };
        }
    }
}
