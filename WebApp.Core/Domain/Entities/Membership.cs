using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Core.Domain.Entities.Enums;

namespace WebApp.Core.Domain.Entities
{
    public class Membership
    {
        private MembershipType _membershipType;
        private DateOnly _membershipStartDate;
        private double _paidAmount;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public MembershipType MembershipType
        {
            get
            {
                return _membershipType;
            }
            set
            {
                _membershipType = value;
                SetMembershipAmount();
            }
        }
        public bool IsMembershipActive { get; set; } = false;

        public DateOnly MembershipStartDate
        {
            get
            {
                return _membershipStartDate;
            }
            set
            {
                _membershipStartDate = value;
                SetMembershipEndDate();
                SetMembershipStatus();
            }
        }
        public DateOnly MembershipEndDate { get; set; }
        public double MembershipAmount { get; set; }
        public double PaidAmount
        {
            get
            {
                return _paidAmount;
            }
            set
            {
                _paidAmount = value;
                DueAmount = MembershipAmount - PaidAmount;
            }
        }
        public double DueAmount { get; set; }
        private void SetMembershipEndDate()
        {
            MembershipEndDate = MembershipType switch
            {
                MembershipType.Monthly => MembershipStartDate.AddMonths(1),
                MembershipType.Quaterly => MembershipStartDate.AddMonths(3),
                MembershipType.Half_Yearly => MembershipStartDate.AddMonths(6),
                MembershipType.Yearly => MembershipStartDate.AddYears(1),
            };
        }
        private void SetMembershipStatus()
        {
            if (MembershipEndDate > DateOnly.FromDateTime(DateTime.Now))
                IsMembershipActive = true;
            else
                IsMembershipActive = false;
        }
        private void SetMembershipAmount()
        {
            MembershipAmount = MembershipType switch
            {
                MembershipType.Monthly => 500,
                MembershipType.Quaterly => 1500,
                MembershipType.Half_Yearly => 3000,
                MembershipType.Yearly => 6000,
            };
        }
        public Member Member { get; set; }
    }
}
