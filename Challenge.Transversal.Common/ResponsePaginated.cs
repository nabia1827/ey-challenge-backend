using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Transversal.Common
{
    public class ResponsePaginated<T>
    {
        public T Data { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
