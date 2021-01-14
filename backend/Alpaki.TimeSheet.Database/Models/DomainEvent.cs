using System;
using System.ComponentModel.DataAnnotations;

namespace Alpaki.TimeSheet.Database.Models
{
    public class DomainEvent<T, E>
        where T : class
        where E : struct
    {
        [Key]
        public Guid EventId { get; set; }

        public DateTime CreatedAt { get; set; }

        public T EventData { get; set; }

        public E EventType { get; set; }
    }
}
