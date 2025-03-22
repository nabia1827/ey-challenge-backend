using Challenge.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Interface
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> ListCountries();

    }
}
