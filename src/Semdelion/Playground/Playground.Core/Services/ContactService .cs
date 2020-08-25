using Playground.Core.Api;
using Playground.Core.Model;
using Refit;
using Semdelion.DAL;
using Semdelion.DAL.Models;
using Semdelion.DAL.Services;
using Semdelion.DAL.Services.Decorators;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Playground.Core.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContactServiceApi _contactService;


        public ContactService(IConnectionService connectionService, IServiceDecorator serviceDecorator) : base(connectionService, serviceDecorator) 
        {
            connectionService._lazyHttpClient.Value.BaseAddress = new Uri("https://randomuser.me/api/");
            _contactService = RestService.For<IContactServiceApi>(
                connectionService._lazyHttpClient.Value, 
                new RefitSettings()
                {
                    ContentSerializer = new SystemTextJsonContentSerializer()
                });
        }

        public async Task<RequestResult<IList<ContactResult>>> GetContacts(int count, int page = 1, ApiMethodContext context = default)
        {
            return await SendApiMethod(() => _contactService.GetContacts(count, page), context);
        }
    }
}
