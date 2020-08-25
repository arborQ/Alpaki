using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Database.Models
{
    public class Sponsor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SponsorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        public string ContactPersonName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Brand { get; set; }

        public SponsorTypeEnum CooperationType { get; set; }

        public virtual ICollection<AssignedSponsor> Dreams { get; set; }
    }
}
