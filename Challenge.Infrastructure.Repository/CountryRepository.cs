using Challenge.Domain.Entity;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DapperContext _context;
        public CountryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> ListCountries()
        {
            using var connection = _context.CreateConnection();

            var sql = "sp_ListCountries";

            var countries = await connection.QueryAsync<Country>(sql, commandType: CommandType.StoredProcedure);
            return countries.ToList();
        }
    }
}
