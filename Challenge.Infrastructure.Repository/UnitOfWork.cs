using Challenge.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository Auth { get; }

        public ISourceRepository Source { get; }

        public ICountryRepository Country { get; }

        public ISupplierRepository Supplier { get; }

        public UnitOfWork(IAuthRepository authRepository, 
            ISourceRepository sourceRepository, ICountryRepository countryRepository,
            ISupplierRepository supplierRepository)
        {
            Auth = authRepository;
            Source = sourceRepository;
            Country = countryRepository;
            Supplier = supplierRepository;
            
        }
    }
}
