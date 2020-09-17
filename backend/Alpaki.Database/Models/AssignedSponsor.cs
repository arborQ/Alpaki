using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    [Table("AssignedSponsor", Schema = "Dreams")]
    public class AssignedSponsor
    {
        [ForeignKey(nameof(Sponsor))]
        public long SponsorId { get; set; }

        public Sponsor Sponsor { get; set; }

        [ForeignKey(nameof(Dream))]
        public long DreamId { get; set; }

        public Dream Dream { get; set; }
    }
}
