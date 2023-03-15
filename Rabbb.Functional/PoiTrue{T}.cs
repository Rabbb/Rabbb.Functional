using System;

namespace Rabbb.Functional
{

    public class PoiTrue<T> : Exception
    {
        public T Value { get; set; }
    }
}
