using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Domain.Entities
{
    public class SupplementOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generated
        public int SupplementOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public Member Member { get; set; }
        public Supplement Supplement { get; set; }
    }
}
