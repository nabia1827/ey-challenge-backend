using Challenge.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Interface
{
    public interface ISourceDomain
    {
        Task<IEnumerable<Source>> ListSources();
    }
}
