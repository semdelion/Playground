using Playground.Core.Model;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Core.Api
{
    public interface  IContactServiceApi
    {
        [Get("/?results={count}&page={page}&seed=1")]
        Task<IList<ContactResult>> GetContacts(int count, int page);
    }
}
