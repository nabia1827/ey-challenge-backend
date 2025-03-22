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
    public interface ISupplierApplication
    {
        Task<ResponsePaginated<IEnumerable<SupplierDto>>> ListSuppliers(int pagina,
            int cantidadRegistros, string legalName,
            string tradeName, string taxIdentNumber, int countryId,
            string initDate, string endDate);

        Task<Response<bool>> UpsertSupplier(int supplierId,
            string legalName,
            string tradeName,
            string taxIdentNumber,
            string phoneNumber,
            string email,
            string website,
            string address,
            int countryId,
            float revenue);

        Task<Response<SupplierDto>> GetSupplier(int supplierId);

        Task<Response<bool>> DeleteSupplier(int supplierId);
    }
}
