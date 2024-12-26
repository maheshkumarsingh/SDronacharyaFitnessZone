using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities.Enums;

namespace WebApp.Core.DTOs
{
    public class AddMembershipRequestDTO
    {
        public string MemberLoginName { get; set; } = string.Empty;
        public MembershipType MembershipType { get; set; }
        //public bool IsOldMember { get; set; }
        public double PaidAmount { get; set; }
        public DateOnly MembershipStartDate { get; set; }

    }
}
