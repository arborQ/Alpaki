using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Alpaki.DomainLogic.ValueObjects
{
    public class DreamId : IEqualityComparer<DreamId>
    {
        public long Value { get; }

        private DreamId() { }
        public DreamId(long value)
        {
            Value = value;
        }

        public bool Equals([AllowNull] DreamId x, [AllowNull] DreamId y)
        {
            return x.Value.Equals(y.Value);
        }

        public int GetHashCode([DisallowNull] DreamId obj)
        {
            return obj.Value.GetHashCode();
        }
    }

    public class PersonFullName : IEqualityComparer<PersonFullName>
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string DisplayFullName { get; }

        private PersonFullName() { }

        public PersonFullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayFullName = $"{firstName}, {lastName}";
        }

        public bool Equals([AllowNull] PersonFullName x, [AllowNull] PersonFullName y)
        {
            return x.DisplayFullName.Equals(y.DisplayFullName);
        }

        public int GetHashCode([DisallowNull] PersonFullName obj)
        {
            return obj.DisplayFullName.GetHashCode();
        }
    }
}
