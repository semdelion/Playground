using Playground.Core.Services;
using Semdelion.API.Models;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using Semdelion.DAL.Providers;
using System.Threading.Tasks;

namespace Playground.Core.Providers
{
    public class ContactProvider: BaseProvider, IContactProvider
    {
        private IContactService _connectionService;
        public ContactProvider(IContactService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<RequestResult<ContactResult>> GetContacts(int count = 5, int page = 1, ApiMethodContext context = default)
        {
             return await PackageServiceResult(() =>_connectionService.GetContacts(count, page, context));
        }
    }
}
