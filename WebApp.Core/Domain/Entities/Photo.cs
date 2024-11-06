using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Core.Domain.Entities
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public Member Member { get; set; } = null!;
    }
}