using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities.Enums;

namespace SDronacharyaFitnessZone.Core.DTOs
{
    public class UpdateMemberRequestDTO
    {
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
        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password doesnot matches with password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string AlternatePhoneNumber { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        [DataType(DataType.Date)]
        public DateOnly JoiningDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public bool IsOldmember
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
        public string ImageUrl { get; set; }
    }
}
