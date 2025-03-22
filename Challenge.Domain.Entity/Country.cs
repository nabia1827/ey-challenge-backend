using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Entity
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool CountryActive { get; set; }
    }
}
