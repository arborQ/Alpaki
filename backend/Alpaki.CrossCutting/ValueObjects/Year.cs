using System;
using System.Collections.Generic;

namespace Alpaki.CrossCutting.ValueObjects
{
    public class Year : ValueObject
    {
        public int Value { get; private set; }

        private Year(int year)
        {
            if (year < 2000)
                throw new ArgumentException($"Value is not valid [{year}]", nameof(year));

            Value = year;
        }

        public static Year From(int year)
        {
            return new Year(year);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static bool operator ==(Year year, int compareYear)
        {
            return year.Value == compareYear;
        }

        public static bool operator !=(Year year, int compareYear)
        {
            return year.Value != compareYear;
        }
    }
}
