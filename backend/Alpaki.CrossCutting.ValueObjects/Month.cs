using System;
using System.Collections.Generic;

namespace Alpaki.CrossCutting.ValueObjects
{
    public class Month : ValueObject
    {
        public int Value { get; private set; }

        public Month(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException($"Value is not valid [{month}]", nameof(month));

            Value = month;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
