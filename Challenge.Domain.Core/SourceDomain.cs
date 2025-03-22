using Challenge.Domain.Entity;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Core
{
    public class SourceDomain : ISourceDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public SourceDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Source>> ListSources()
        {
            return await _unitOfWork.Source.ListSources();
        }
    }
}
