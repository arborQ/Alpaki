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

            BrandDomainEvents = new HashSet<BrandDomainEvent>()
            {
                new BrandDomainEvent()
            };
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BrandId { get; set; }

        [MaxLength(500)]
        [Required]
        public string BrandName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Model> Models { get; set; }

        public virtual ICollection<BrandDomainEvent> BrandDomainEvents { get; set; }
    }
}
