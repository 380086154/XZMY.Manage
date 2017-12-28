using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2M.Common.Utils.Models
{
    public class CompareResult<T>
    {
        public CompareResult()
        {
            AddedElements = new T[0];
            RemovedElements = new T[0];
        }
        public T[] AddedElements { get; set; }
        public T[] RemovedElements { get; set; }
    }
}
