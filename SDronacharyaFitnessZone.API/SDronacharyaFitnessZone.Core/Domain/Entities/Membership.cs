using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;
public class Membership
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generated
    public int Id { get; set; }
    private MembershipType _membershipType;
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
    public bool IsMembershipActive { get; set; }

    private DateOnly _membershipStartDate;
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
        }
    }
    public DateOnly MembershipEndDate { get; set; }
    public double MembershipAmount { get; set; }
    public Member Member { get; set; }
    private void SetMembershipEndDate()
    {
        MembershipEndDate = MembershipType switch
        {
            MembershipType.Monthly => MembershipStartDate.AddMonths(1),
            MembershipType.Quaterly => MembershipStartDate.AddMonths(3),
            MembershipType.Yearly => MembershipStartDate.AddYears(1),
        };
    }
    private void SetMembershipAmount()
    {
        MembershipAmount = MembershipType switch
        {
            MembershipType.Monthly => 500,
            MembershipType.Quaterly => 2000,
            MembershipType.Yearly => 6000,
        };
    }
}
