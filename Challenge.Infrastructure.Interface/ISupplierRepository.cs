using Challenge.Domain.Entity;
using Challenge.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Interface
{
    public interface ISupplierRepository
    {
        Task<PagedResults<Supplier>> ListSuppliers(int pagina,
            int cantidadRegistros,string legalName, 
            string tradeName, string taxIdentNumber, int countryId, 
            string initDate, string endDate);

        Task<bool> UpsertSupplier(int supplierId,
            string legalName,
            string tradeName,
            string taxIdentNumber,
            string phoneNumber,
            string email,
            string website,
            string address,
            int countryId,
            float revenue);

        Task<Supplier> GetSupplier(int supplierId);

        Task<bool> DeleteSupplier(int supplierId);

    }
}
