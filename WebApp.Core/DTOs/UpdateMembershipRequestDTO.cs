using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.Entities.Enums;

namespace WebApp.Core.DTOs
{
    public class UpdateMembershipRequestDTO
    {
        public int Id { get; set; }
        public string MemberLoginName { get; set; } = string.Empty;
        public MembershipType MembershipType { get; set; }
        public double PaidAmount { get; set; }
        public DateOnly MembershipStartDate { get; set; }
    }
}
