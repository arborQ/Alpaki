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

        public string Tags { get; set; }

        [ForeignKey(nameof(Dreamer))]
        public long DreamerId { get; set; }

        public Dreamer Dreamer { get; set; }

        [ForeignKey(nameof(DreamCategory))]
        public long DreamCategoryId { get; set; }

        public DreamCategory DreamCategory { get; set; }

        public DateTimeOffset DreamComeTrueDate { get; set; }

        public DreamStateEnum DreamState { get; set; }

        public virtual ICollection<DreamStep> RequiredSteps { get; set; }

        public virtual ICollection<AssignedDreams> Volunteers { get; set; }

    }
}
