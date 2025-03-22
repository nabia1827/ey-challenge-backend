using Challenge.Application.DTO;
using Challenge.Domain.Entity;
using Challenge.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Interface
{
    public interface ISourceApplication
    {
        Task<Response<IEnumerable<SourceDto>>> ListSources();
    }
}
