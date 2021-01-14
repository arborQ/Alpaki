using System;
using System.Collections.Generic;

namespace Alpaki.CrossCutting.ValueObjects
{
    public class Year : ValueObject
    {
        public int Value { get; private set; }

        public Year(int year)
        {
            if (year < 2000)
                throw new ArgumentException($"Value is not valid [{year}]", nameof(year));

            Value = year;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
