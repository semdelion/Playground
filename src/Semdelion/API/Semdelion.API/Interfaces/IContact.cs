using Refit;
using Semdelion.API.Models;
using System.Threading.Tasks;

namespace Semdelion.API.Interfaces
{
    public interface IContact
    {
        [Get("/?results={count}&page={page}&seed=1")]
        Task<ContactResult> GetContacts(int count, int page);
    }
}
