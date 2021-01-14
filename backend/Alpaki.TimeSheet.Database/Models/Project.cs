using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.TimeSheet.Database.Models
{
    [Table("Project", Schema = "TimeSheet")]
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
