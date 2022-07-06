using System.Threading.Tasks;
using Buy.Idp.DataAccess.Domain;

namespace Buy.Idp.Business.Providers
{
    public interface IUserProvider {
        Task<User> GetBySubjectAsync(string subject);
        Task<User> GetByEmailAsync(string email);
    }
}