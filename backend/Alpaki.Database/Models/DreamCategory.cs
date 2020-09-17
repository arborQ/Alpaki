using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    [Table("DreamCategory", Schema = "Dreams")]
    public class DreamCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DreamCategoryId { get; set; }

        [MaxLength(250)]
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Dream> Dreams { get; set; }

        public virtual ICollection<DreamCategoryDefaultStep> DefaultSteps { get; set; }
    }
}
