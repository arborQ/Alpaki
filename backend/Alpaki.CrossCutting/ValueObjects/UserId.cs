using System;

namespace Alpaki.CrossCutting.ValueObjects
{
    public class UserId : PrimitivValueObject<long>
    {
        public UserId(long value) : base(value)
        {
            if (value == default)
            {
                throw new ArgumentException($"UserId must have value");
            }
        }
    }
}
