using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Entity
{
    public class Source
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceWeb { get; set; }
        public bool SourceActive { get; set; }
    }
}
