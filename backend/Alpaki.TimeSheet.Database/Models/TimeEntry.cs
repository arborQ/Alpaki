using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.ValueObjects;

namespace Alpaki.TimeSheet.Database.Models
{
    [Table(nameof(TimeEntry), Schema = "TimeSheet")]
    public class TimeEntry
    {
        public TimeSheetPeriod TimeSheet { get; set; }

        public Year Year { get; set; }

        public Month Month { get; set; }

        public int Day { get; set; }

        public UserId UserId { get; set; }

        public decimal Hours { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public long? ProjectId { get; set; }
    }
}
