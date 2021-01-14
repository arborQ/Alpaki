using System.Collections.Generic;

namespace Alpaki.CrossCutting.ValueObjects
{
    public abstract class PrimitivValueObject<T> : ValueObject
    {
        public T Value { get; protected set; }

        public PrimitivValueObject(T value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
