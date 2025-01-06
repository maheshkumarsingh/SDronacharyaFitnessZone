using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Domain.Entities
{
    public class MemberMentor
    {
        public Member Mentor { get; set; } = null!;
        public string MentorLoginName { get; set; } = string.Empty;
        public Member Member { get; set; } = null!;
        public string MemberLoginName { get; set; } = string.Empty;
    }
}
