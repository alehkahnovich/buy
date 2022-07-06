using System.Threading.Tasks;
using Buy.Idp.DataAccess.Connections;
using Buy.Idp.DataAccess.Domain;
using Buy.Idp.DataAccess.Representations.AccessType;
using Dapper;

namespace Buy.Idp.Business.Providers
{
    public sealed class UserProvider : IUserProvider {
        private readonly IConnectionFactory _connectionFactory;
        public UserProvider(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<User> GetByEmailAsync(string email) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.QuerySingleOrDefaultAsync<User>("select * from IDP.[User] where Email = @Email", new { Email = email })
                    .ConfigureAwait(false);
            }
        }

        public async Task<User> GetBySubjectAsync(string subject) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.QuerySingleOrDefaultAsync<User>("select * from IDP.[User] where UserId = @UserId", new { UserId = subject })
                    .ConfigureAwait(false);
            }
        }
    }
}