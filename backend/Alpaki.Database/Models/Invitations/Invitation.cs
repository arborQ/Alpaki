using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Database.Models.Invitations
{
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
        public DateTimeOffset Timestamp { get; set; }
    }

}