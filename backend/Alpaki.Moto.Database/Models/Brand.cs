using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Moto.Database.Models
{
    [Table("Brand", Schema = "Moto")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BrandId { get; set; }

        [MaxLength(500)]
        [Required]
        public string BrandName { get; set; }
    }
}
