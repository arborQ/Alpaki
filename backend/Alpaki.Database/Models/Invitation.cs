using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Database.Models
{
    [Table("Invitation", Schema = "Dreams")]
    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvitationId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(4)]
        public string Code { get; set; }
        [Required]
        [DefaultValue(InvitationStateEnum.Pending)]
        public InvitationStateEnum Status { get; set; }
        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Attempts { get; set; }
    }

}