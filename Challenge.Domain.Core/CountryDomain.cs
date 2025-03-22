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
    public class CountryDomain : ICountryDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Country>> ListCountries()
        {
            return await _unitOfWork.Country.ListCountries();
        }
    }
}
