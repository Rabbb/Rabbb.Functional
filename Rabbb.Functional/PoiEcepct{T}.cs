using System;

namespace Rabbb.Functional
{

    public class PoiEcepct<T> : Exception
        where T : Exception
    {
        public T Value { get; set; }
    }
}
