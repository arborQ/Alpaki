using Alpaki.CrossCutting.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    public class Dreamer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DreamerId { get; set; }

        [MaxLength(250)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(250)]
        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public virtual ICollection<Dream> Dreams { get; set; }

        public string DreamUrl { get; set; }
    }
}
