using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDronacharyaFitnessZone.Core.Domain.Entities
{
    public class Gym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GymId { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage ="No special characters allowed.")]
        public string GymName { get; set; } = "S Dronacharya Fitness Zone";
        [Required]
        [StringLength(50)]
        public string GymDescription { get; set; }
        public string GymOffers { get ; set; }
        public string GymAddress { get; set; }
        public string GymContactNumber { get; set; }
        public IList<Member> Members { get; set; }

    }
}
