using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbb.Functional
{
    public class PoiTask<T>
    {
        private Lazy<T> m_Result;
        public PoiTask(Func<T> result_fn)
        {
            this.m_Result = new Lazy<T>(result_fn);
        }

        public T Result => m_Result.Value;
    }

}