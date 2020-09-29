using System;

namespace Alpaki.Common.Jobs.Models
{
    public class BusinessEventModel<T>
    {
        public Guid EventId { get; set; }

        public string EventType { get; set; }

        public T Message { get; set; }
    }
}
