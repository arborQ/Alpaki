using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Moto.Database.Models
{
    [Table("Brand", Schema = "Moto")]
    public class Brand
    {
        private Brand() { }

        public Brand(string brandName)
        {
            BrandName = brandName;
            IsActive = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BrandId { get; private set; }

        [MaxLength(500)]
        [Required]
        public string BrandName { get; private set; }

        public bool IsActive { get; private set; }

        public virtual ICollection<Model> Models { get; private set; }
    }
}
