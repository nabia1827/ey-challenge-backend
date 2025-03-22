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
    public class SourceRepository : ISourceRepository
    {
        private readonly DapperContext _context;
        public SourceRepository(DapperContext context)
        {
            _context = context;
        } 
        public async Task<IEnumerable<Source>> ListSources()
        {
            using var connection = _context.CreateConnection();

            var sql = "sp_ListSources";

            var sources = await connection.QueryAsync<Source>(sql, commandType: CommandType.StoredProcedure);
            return sources.ToList();
        }
    }
}
