using System.ComponentModel.DataAnnotations.Schema;

namespace Alpaki.TimeSheet.Database.Models
{
    [Table("TimeEntry", Schema = "TimeSheet")]
    public class TimeEntry
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public long UserId { get; set; }

        public decimal Hours { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public long? ProjectId { get; set; }
    }

    [Table("Project", Schema = "TimeSheet")]
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
