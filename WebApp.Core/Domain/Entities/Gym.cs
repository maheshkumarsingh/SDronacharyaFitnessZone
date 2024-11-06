using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Domain.Entities
{
    public class Gym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GymId { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "No special characters allowed.")]
        public string GymName { get; set; } = "S Dronacharya Fitness Zone";
        [Required]
        [StringLength(50)]
        public string GymDescription { get; set; } = string.Empty;
        public string GymOffers { get; set; } = string.Empty;
        public string GymAddress { get; set; } = string.Empty ;
        public string GymContactNumber { get; set; } = string.Empty;
        public IList<Member> Members { get; set; } = [];
        public IList<Maintenance> Maintenances { get; set; } = [];

    }
}
