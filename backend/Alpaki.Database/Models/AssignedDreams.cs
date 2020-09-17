using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    [Table("AssignedDreams", Schema = "Dreams")]
    public class AssignedDreams
    {
        [ForeignKey(nameof(Volunteer))]
        public long VolunteerId { get; set; }

        public User Volunteer { get; set; }

        [ForeignKey(nameof(Dream))]
        public long DreamId { get; set; }

        public Dream Dream { get; set; }
    }
}
