using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alpaki.CrossCutting.Enums;

namespace Alpaki.Moto.Database.Models
{
    [Table("BrandDomainEvent", Schema = "Moto")]
    public class BrandDomainEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BrandDomainEventId { get; set; }

        public DomainEventType DomainEventType { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Processed { get; set; }

        public long BrandId { get; set; }

        public Brand Brand { get; set; }

        private BrandDomainEvent() { }

        public BrandDomainEvent(DomainEventType domainEventType = DomainEventType.Create)
        {
            BrandDomainEventId = Guid.NewGuid();
            DomainEventType = domainEventType;
            Created = DateTimeOffset.UtcNow;
            Processed = null;
        }

        public override int GetHashCode() => (DomainEventType, Processed).GetHashCode();
    }
}
