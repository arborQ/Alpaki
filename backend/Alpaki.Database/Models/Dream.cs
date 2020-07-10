using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Database.Models
{
    public class Dream
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DreamId { get; set; }

        [MaxLength(250)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(250)]
        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public string DreamUrl { get; set; }

        public string Tags { get; set; }

        [ForeignKey(nameof(DreamCategory))]
        public long DreamCategoryId { get; set; }

        public DreamCategory DreamCategory { get; set; }

        public DateTimeOffset DreamComeTrueDate { get; set; }

        public DreamStateEnum DreamState { get; set; }

        public virtual ICollection<DreamStep> RequiredSteps { get; set; }

        public virtual ICollection<AssignedDreams> Volunteers { get; set; }

    }
}
