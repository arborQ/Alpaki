using System;

namespace Alpaki.Logic
{
    public abstract class LogicException : Exception
    {
        public abstract string Code { get; }
        public string Reason { get; }
        protected LogicException(string reason):base(reason)
        {
            Reason = reason;
        }
    }
}