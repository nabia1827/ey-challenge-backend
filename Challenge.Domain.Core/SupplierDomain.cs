using Challenge.Domain.Entity;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Interface;
using Challenge.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Core
{
    public class SupplierDomain : ISupplierDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteSupplier(int supplierId)
        {
            return await _unitOfWork.Supplier.DeleteSupplier(supplierId);
        }

        public async Task<Supplier> GetSupplier(int supplierId)
        {
            return await _unitOfWork.Supplier.GetSupplier(supplierId);
        }

        public async Task<PagedResults<Supplier>> ListSuppliers(int pagina, int cantidadRegistros, string legalName, string tradeName, string taxIdentNumber, int countryId, string initDate, string endDate)
        {
            return await _unitOfWork.Supplier.ListSuppliers(
                pagina,cantidadRegistros,legalName,
                tradeName, taxIdentNumber, countryId, initDate, endDate);    
        }

        public async Task<bool> UpsertSupplier(int supplierId, string legalName, string tradeName, string taxIdentNumber, string phoneNumber, string email, string website, string address, int countryId, float revenue)
        {
            return await _unitOfWork.Supplier.UpsertSupplier(supplierId,
                legalName, tradeName,taxIdentNumber,phoneNumber, 
                email, website, address, countryId, revenue);
        }
    }
}
