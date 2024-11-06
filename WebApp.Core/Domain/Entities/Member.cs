using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Core.Domain.Entities.Enums;
using WebApp.Core.Domain.Utility;
using WebApp.Core.DTOs;

namespace WebApp.Core.Domain.Entities
{
    public class Member
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Key]
        [Required]
        public string MemberLoginName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public byte[] Password { get; set; } = [];
        [Required]
        public byte[] PasswordSalt { get; set; } = [];
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

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
        public List<Membership>? Memberships { get; set; } = [];
        public List<SupplementOrder>? SupplementOrders { get; set; } = [];
        public IList<Photo> Photos { get; set; } = [];
        public int GymId { get; set; } = 1;

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }

}