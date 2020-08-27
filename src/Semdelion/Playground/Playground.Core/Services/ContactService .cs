
using Refit;
using Semdelion.API.Interfaces;
using Semdelion.API.Models;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using Semdelion.DAL.Services;
using Semdelion.DAL.Services.Decorators;
using System;
using System.Threading.Tasks;

namespace Playground.Core.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContact _contactService;


        public ContactService(IConnectionService connectionService, IServiceDecorator serviceDecorator) : base(connectionService, serviceDecorator) 
        {
            connectionService._lazyHttpClient.Value.BaseAddress = new Uri("https://randomuser.me/api/");
            _contactService = RestService.For<IContact>(connectionService._lazyHttpClient.Value);
        }

        public async Task<RequestResult<ContactResult>> GetContacts(int count, int page = 1, ApiMethodContext context = default)
        {
            return await SendApiMethod(() => _contactService.GetContacts(count, page), context);
        }
    }
}
