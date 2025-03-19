using Challenge.Domain.Entity;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Interface;
using Dapper;
using System.Data;

namespace Challenge.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;
        public AuthRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            using var connection = _context.CreateConnection();

            var sql = "sp_AuthenticateUser";
            var parameters = new DynamicParameters();
            parameters.Add("@username", username);
            parameters.Add("@password", password);

            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters, commandType: CommandType.StoredProcedure);
            return user;
        }

        public async Task<User> GetHashPasswordByUsername(string username)
        {
            using var connection = _context.CreateConnection();

            var sql = "sp_GetHashPasswordByUsername";
            var parameters = new DynamicParameters();
            parameters.Add("@username", username);

            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters, commandType: CommandType.StoredProcedure);
            return user;
        }

        public async Task<bool> SaveRefreshToken(int userId, string token, string refreshToken)
        {
            var sql = "sp_InsertUserRefreshToken";

            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            parameters.Add("@token", token);
            parameters.Add("@refreshToken", refreshToken);

            var result = await connection.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<int> VerifyRefreshToken(string expiredToken, string refreshToken, int userId)
        {
            var sp = "sp_VerifyRefreshToken";
            var parameters = new DynamicParameters();
            parameters.Add("@token", expiredToken);
            parameters.Add("@refreshToken", refreshToken);
            parameters.Add("@userId", userId);

            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
