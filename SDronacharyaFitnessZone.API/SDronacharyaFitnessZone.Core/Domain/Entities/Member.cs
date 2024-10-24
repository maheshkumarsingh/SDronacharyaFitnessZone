using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SDronacharyaFitnessZone.Core.Domain.Entities;
using SDronacharyaFitnessZone.Core.Domain.Entities.Enums;

public class Member
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generated
    public int Id { get; set; }

    [StringLength(50)]
    [Required]
    [Key]
    public string MemberLoginName { get; set; }

    [Required]
    [StringLength(50)]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
    public string FirstName { get; set; }

    [StringLength(50)]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
    public string MiddleName { get; set; }

    [Required]
    [StringLength(50)]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [DataType(DataType.Text)]
    public int Age => (int)(DateTime.Now.Year - DateOfBirth.Year);

    [DataType(DataType.Text)]
    [Required]
    public Gender Gender { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public MembershipType MembershipType { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character.")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Confirm Password doesnot matches with password")]
    public string? ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string? AlternatePhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? BloodGroup { get; set; }
    [DataType(DataType.Date)]
    public DateOnly JoiningDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool IsOldMember
    {
        get
        {
            if (JoiningDate.Year < DateTime.Now.Year - 2)
            {
                return true;
            }
            return false;
        }
    }
    public List<Membership>? Memberships { get; set; }
    public List<SupplementOrder>? SupplementOrders { get; set; }
    public int GymId { get; set; }
    public string ImageUrl { get; internal set; }
    public override string ToString()
    {
        return $"Id: {Id}MemberLoginName: {MemberLoginName}\nFirstName: {FirstName}\nLastName: {LastName}\nDateOfBirth: {DateOfBirth}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}\nAlternatePhoneNumber: {AlternatePhoneNumber}\nAddress: {Address}\nBloodGroup: {BloodGroup}\nJoiningDate: {JoiningDate}";
    }
}
