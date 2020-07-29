using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    public class DreamCategoryDefaultStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DreamCategoryDefaultStepId { get; set; }

        [Required]
        public string StepDescription { get; set; }

        [DefaultValue(false)]
        public bool IsSponsorRelated { get; set; }

        [ForeignKey(nameof(DreamCategory))]
        public long DreamCategoryId { get; set; }

        public DreamCategory DreamCategory { get; set; }
    }
}
