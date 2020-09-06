namespace Semdelion.DAL.Services
{
    using Semdelion.DAL.Models;
    using Semdelion.DAL.Services.Attributes;
    using Semdelion.DAL.Services.Decorators;
    using Semdelion.DAL.Services.Extensions;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class BaseService
    {
        private ServiceContext _serviceContext;
        private IServiceDecorator ServiceDecorator { get; }

        protected IConnectionService ConnectionService { get; }

        private ServiceContext Context => _serviceContext ??= GetServiceContext();

        protected BaseService(IConnectionService connectionService, IServiceDecorator serviceDecorator)
        {
            ConnectionService = connectionService;
            ServiceDecorator = serviceDecorator;
        }

        protected Task<RequestResult<TResult>> SendApiMethod<TResult>(
            Func<Task<TResult>> apiFunc,
            ApiMethodContext apiContext,
            [CallerMemberName] string callerMethodName = null) where TResult : class
        {
            var context = GetServiceMethodContext(callerMethodName) ?? Context;
            return ServiceDecorator.SendApiMethod(context, apiFunc, apiContext);
        }

        private ServiceContext GetServiceContext()
        {
            var currentType = GetType();

            if (currentType.GetCustomAttributes(typeof(ServiceAttribute), true)?.FirstOrDefault() is ServiceAttribute serviceAttribute)
            {
                return serviceAttribute.GetServiceContext(GetType());
            }

            foreach (var interfaceType in currentType.GetInterfaces())
            {
                if (interfaceType.GetCustomAttributes(typeof(ServiceAttribute), true)?.FirstOrDefault() is ServiceAttribute
                    intServiceAttribute)
                {
                    return intServiceAttribute.GetServiceContext(GetType());
                }
            }

            return new ServiceContext(GetType()) { CheckAuth = true };
        }

        private ServiceContext GetServiceMethodContext(string callerMethodName)
        {
            var currentType = GetType();
            MethodInfo methodInfo = null;

            foreach (var interfaceType in currentType.GetInterfaces())
            {
                methodInfo = interfaceType.GetMethod(callerMethodName);
                if (methodInfo != null)
                {
                    break;
                }
            }

            methodInfo ??= currentType.GetMethod(callerMethodName);
            if (methodInfo != null &&
                methodInfo.GetCustomAttributes(typeof(ServiceAttribute), true)?.FirstOrDefault() is ServiceAttribute serviceAttribute)
            {
                return serviceAttribute.GetServiceContext(GetType());
            }

            return null;
        }
    }
}
