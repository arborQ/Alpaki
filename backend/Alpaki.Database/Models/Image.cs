using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.Database.Models
{
    [Table("Image", Schema = "Shared")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }

        public DateTime Created { get; set; }

        public byte[] ImageData { get; set; }
    }
}
