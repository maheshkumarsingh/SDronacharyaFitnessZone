using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.DTOs
{
    public class AddMembershipRequestDTO
    {
        public string MemberLoginName { get; set; }
        public MembershipType MembershipType { get; set; }
        public bool IsOldMember { get; set; }
    }
}
