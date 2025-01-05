using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.Entities.Enums;

namespace WebApp.Core.DTOs
{
    public class MembershipPlanResponseDTO
    {
        public int Id { get; set; }
        public MembershipType Type { get; set; }
        public string? MembershipName { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
    }

    public static class MembershipPlanExtension
    {
        public static MembershipPlanResponseDTO ToMembershipPlanResponseDTO(this MembershipPlan plan)
        {
            return new MembershipPlanResponseDTO
            {
                Id = plan.Id,
                Type = plan.MembershipTypeId,
                MembershipName = plan.MembershipTypeId.ToString(),
                Duration = plan.Duration,
                Price = plan.Price,
            };
        }
    }
}
