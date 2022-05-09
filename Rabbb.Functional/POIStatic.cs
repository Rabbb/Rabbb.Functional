using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbb.Functional
{
    public static class POIStatic
    {
        /// <summary>
        /// Return a result that is true. <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static POI<T, F> True<T, F>(T @true)
        {
            return new POI<T, F>() { Resolve = @true };
        }

        /// <summary>
        /// Return a result that is false.  <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static POI<T, F> False<T, F>(F @false)
        {
            return new POI<T, F>() { Reject = @false };
        }

        /// <summary>
        /// Return a result that neither true or false, which is a program exception.  <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static POI<T, F> Except<T, F>(Exception ex)
        {
            return new POI<T, F>() { Exception = ex };
        }
    }
}
