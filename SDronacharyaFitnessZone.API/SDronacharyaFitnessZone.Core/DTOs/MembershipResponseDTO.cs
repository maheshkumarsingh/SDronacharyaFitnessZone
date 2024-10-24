using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;
using SDronacharyaFitnessZone.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                MemberLoginName = membership.Member.MemberLoginName,
            };
        }
    }
}
