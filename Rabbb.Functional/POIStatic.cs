using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbb.Functional
{
    public static class POIStatic
    {
        public static POI<T, F> True<T, F>(T @true)
        {
            return new POI<T, F>() { Resolve = @true };
        }

        public static POI<T, F> False<T, F>(F @false)
        {
            return new POI<T, F>() { Reject = @false };
        }
    }
}
