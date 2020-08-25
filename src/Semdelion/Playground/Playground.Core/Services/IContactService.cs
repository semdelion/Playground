using Playground.Core.Model;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Core.Services
{
    public interface IContactService
    {
        Task<RequestResult<IList<ContactResult>>> GetContacts(int count, int page, ApiMethodContext context = default);
    }
}
