using Semdelion.API.Models;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using System.Threading.Tasks;

namespace Playground.Core.Providers
{
    public interface IContactProvider
    {
        public Task<RequestResult<ContactResult>> GetContacts(int count = 5, int page = 1, ApiMethodContext context = default);
    }
}
