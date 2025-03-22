using Challenge.Domain.Entity;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Interface;
using Challenge.Transversal.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DapperContext _context;
        public SupplierRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSupplier(int supplierId)
        {
            try
            {
                var sql = @"sp_DeleteSupplier";

                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@supplierId", supplierId);

                var result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Supplier> GetSupplier(int supplierId)
        {
            using var connection = _context.CreateConnection();

            var sql = "sp_GetSupplier";
            var parameters = new DynamicParameters();
            parameters.Add("@supplierId", supplierId);

            var supplier = await connection.QueryFirstAsync<Supplier>(sql, parameters, commandType: CommandType.StoredProcedure);
            return supplier;
        }

        public async Task<PagedResults<Supplier>> ListSuppliers(int pagina, int cantidadRegistros, 
            string legalName, string tradeName, string taxIdentNumber, 
            int countryId, string initDate, string endDate)
        {
            var results = new PagedResults<Supplier>();

            using var connection = _context.CreateConnection();

            var sql = "sp_ListCountries";

            var count = "sp_CountSuppliers";
            var parameters = new DynamicParameters();
            parameters.Add("@offSet", (pagina - 1) * cantidadRegistros);
            parameters.Add("@pageSize", cantidadRegistros);
            parameters.Add("@legalName", legalName);
            parameters.Add("@tradeName", tradeName);
            parameters.Add("@taxIdentNumber", taxIdentNumber);
            parameters.Add("@countryId", countryId);
            parameters.Add("@countryId", initDate);
            parameters.Add("@countryId", endDate);

            var items = await connection.QueryAsync<Supplier>(sql,parameters, commandType: CommandType.StoredProcedure);
            var total = await connection.ExecuteScalarAsync<int>(count, parameters, commandType: CommandType.StoredProcedure);


            results.Items = items.ToList();
            results.TotalSize = total;

            return results;
        }

        public async Task<bool> UpsertSupplier(int supplierId, string legalName, 
            string tradeName, string taxIdentNumber, string phoneNumber, 
            string email, string website, string address, int countryId, 
            float revenue)
        {
            try
            {
                var sql = @"sp_UpsertSupplier";

                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@supplierId", supplierId);
                parameters.Add("@legalName", legalName);
                parameters.Add("@tradeName", tradeName);
                parameters.Add("@taxIdentNumber", taxIdentNumber);
                parameters.Add("@phoneNumber", phoneNumber);
                parameters.Add("@email", email);
                parameters.Add("@website", website);
                parameters.Add("@address", address);
                parameters.Add("@countryId", countryId);
                parameters.Add("@revenue", revenue);

                var result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
