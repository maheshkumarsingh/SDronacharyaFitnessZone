using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Domain.Entities
{
    public class Supplement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generated
        public int SupplementId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [Required]
        public string Weight { get; set; } = null!;
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Gym Gym { get; set; }
    }
}
