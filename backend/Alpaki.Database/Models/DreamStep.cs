using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Database.Models
{
    [Table("DreamStep", Schema = "Dreams")]
    public class DreamStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DreamStepId { get; set; }

        [Required]
        public string StepDescription { get; set; }

        [DefaultValue(StepStateEnum.Awaiting)]
        public StepStateEnum StepState { get; set; }


        [ForeignKey(nameof(Dream))]
        public long DreamId { get; set; }

        public Dream Dream { get; set; }
    }
}
