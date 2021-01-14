using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.ValueObjects;

namespace Alpaki.TimeSheet.Database.Models
{
    [Table(nameof(TimeSheetPeriod), Schema = "TimeSheet")]
    public class TimeSheetPeriod
    {
        public Year Year { get; set; }

        public Month Month { get; set; }

        public UserId UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LockedFrom { get; set; }

        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
