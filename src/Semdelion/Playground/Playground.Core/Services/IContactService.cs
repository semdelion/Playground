using Semdelion.API.Models;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using System.Threading.Tasks;

namespace Playground.Core.Services
{
    public interface IContactService
    {
        Task<RequestResult<ContactResult>> GetContacts(int count, int page, ApiMethodContext context = default);
    }
}
