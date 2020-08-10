using Alpaki.DomainLogic.ValueObjects;

namespace Alpaki.DomainLogic.DomainObjects
{
    public class DreamDomainObject
    {
        //public DreamId DreamId { get; }

        public PersonFullName FullName { get; }

        private DreamDomainObject() { }
        public DreamDomainObject(long dreamId, string firstName, string lastName)
        {
            //DreamId = new DreamId(dreamId);
            FullName = new PersonFullName(firstName, lastName);
        }
    }
}
