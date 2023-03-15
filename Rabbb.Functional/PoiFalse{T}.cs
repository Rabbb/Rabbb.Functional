using System;

namespace Rabbb.Functional
{
    public class PoiFalse<T> : Exception
    {
        public T Value { get; set; }
    }
}
