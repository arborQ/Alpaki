using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alpaki.CrossCutting.ValueObjects;

namespace Alpaki.TimeSheet.Database.Models
{
    [Table(nameof(TimeSheetPeriod), Schema = "TimeSheet")]
    public class TimeSheetPeriod
    {
        public Year Year { get; private set; }

        public Month Month { get; private set; }

        public UserId UserId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? LockedFrom { get; private set; }

        public virtual ICollection<TimeEntry> TimeEntries { get; private set; }

        public virtual ICollection<DomainEvent<TimeSheetEventData, TimeSheetEventType>> DomainEvents { get; set; }

        private TimeSheetPeriod() { }

        public TimeSheetPeriod(Year year, Month month, UserId userId, IEnumerable<TimeEntry> timeEntries)
        {
            Year = year;
            Month = month;
            UserId = userId;
            LockedFrom = null;

            TimeEntries = timeEntries.ToList();
            DomainEvents = new List<DomainEvent<TimeSheetEventData, TimeSheetEventType>> {
                new DomainEvent<TimeSheetEventData, TimeSheetEventType> {
                    CreatedAt = DateTime.UtcNow,
                    EventType = TimeSheetEventType.Created,
                    EventData = new TimeSheetEventData 
                    {
                        TotalHours = timeEntries.Sum(e => e.Hours)
                    }
                }
            };
        }

        public void UpdateHours(IReadOnlyCollection<TimeEntry> newTimeEntries)
        {
            var commonEntries = newTimeEntries.Join(TimeEntries, a => (a.Day, a.Hours), a => (a.Day, a.Hours), (a, b) => a).ToList();

            if (commonEntries.Count != TimeEntries.Count || commonEntries.Count != newTimeEntries.Count)
            {
                DomainEvents.Add(new DomainEvent<TimeSheetEventData, TimeSheetEventType>
                {
                    EventType = TimeSheetEventType.Updated,
                    CreatedAt = DateTime.UtcNow,
                    EventData = new TimeSheetEventData { TotalHours = newTimeEntries.Sum(e => e.Hours) }
                });
                TimeEntries = newTimeEntries.ToList();
            }
        }

        public void Lock()
        {
            DomainEvents.Add(new DomainEvent<TimeSheetEventData, TimeSheetEventType> { EventType = TimeSheetEventType.Locked, CreatedAt = DateTime.UtcNow, EventData = new TimeSheetEventData { } });
        }

        public void Unlock()
        {
            DomainEvents.Add(new DomainEvent<TimeSheetEventData, TimeSheetEventType> { EventType = TimeSheetEventType.Unlocked, CreatedAt = DateTime.UtcNow, EventData = new TimeSheetEventData { } });
        }
    }
}
