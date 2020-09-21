using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Moto.Database.Models
{
    [Table("Model", Schema = "Moto")]
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ModelId { get; set; }

        [MaxLength(500)]
        [Required]
        public string ModelName { get; set; }

        [ForeignKey(nameof(Brand))]
        public long BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
