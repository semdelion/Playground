using MvvmCross;
using Polly;
using Semdelion.DAL.Models;
using Semdelion.DAL.Services.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Semdelion.DAL.Services.Decorators
{
    /// <summary>
    ///     Обертка для сервисов.
    /// </summary>
    public sealed class ServiceDecorator : IServiceDecorator
    {
        private IDictionary<Type, Lazy<IServiceFilter>> _singletonFilters;

        private IDictionary<Type, Lazy<IServiceFilter>> SingletonFilters => _singletonFilters ??= new Dictionary<Type, Lazy<IServiceFilter>>();

        /// <summary>
        ///     Список фильтров для сервисов.
        /// </summary>
        public IList<IServiceFilter> Filters => new ReadOnlyCollection<IServiceFilter>(GetServiceFilters());

        /// <summary>
        ///     Добавить фильтр.
        /// </summary>
        /// <typeparam name="T">Тип добавляемого фильтра.</typeparam>
        public void AddFilter<T>()
            where T : class, IServiceFilter
        {
            AddFilter(CreateServiceFilter<T>);
        }

        /// <summary>
        ///     Добавить фильтр.
        /// </summary>
        /// <param name="filterFunc">Делегат фильтра.</param>
        /// <typeparam name="T">Тип добавляемого фильтра.</typeparam>
        public void AddFilter<T>(Func<T> filterFunc)
            where T : IServiceFilter
        {
            if (filterFunc == null)
            {
                System.Diagnostics.Debug.WriteLine("Фильтр не задан");
                return;
            }

            SingletonFilters[typeof(T)] = new Lazy<IServiceFilter>(() => filterFunc());
        }

        /// <summary>
        ///     Удалить фильтр.
        /// </summary>
        /// <typeparam name="T">Тип удаляемого фильтра.</typeparam>
        public void RemoveFilter<T>()
            where T : IServiceFilter
        {
            var type = typeof(T);
            if (SingletonFilters.ContainsKey(type))
            {
                SingletonFilters.Remove(type);
            }
        }

        /// <summary>
        ///     Выполнить метод.
        /// </summary>
        /// <param name="serviceContext">Контекст сервиса.</param>
        /// <param name="apiFunc">Делегат выполняемого метода.</param>
        /// <param name="apiContext">Апи контекст.</param>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <returns>Результат выполненного метода.</returns>
        public Task<RequestResult<TResult>> SendApiMethod<TResult>(
            ServiceContext serviceContext,
            Func<Task<TResult>> apiFunc,
            ApiMethodContext apiContext) 
            where TResult : class
        {
            return SendApiMethodInternal(serviceContext, apiFunc, apiContext);
        }


        private async Task<RequestResult<Response>> SendApiMethodInternal<Response>(
            ServiceContext serviceContext,
            Func<Task<Response>> apiMethodFunc, 
            ApiMethodContext apiContext)
            where Response : class
        {
            apiContext ??= ApiMethodContext.Default;
            var filters = GetServiceFilters(serviceContext.IncludeFilters, serviceContext.ExcludeFilters);

            try
            {
                return await Policy
                    .Handle<ApiMethodException>()
                    .WaitAndRetryAsync(
                        serviceContext.MaxRetryCount, 
                        retryNumber => TimeSpan.FromMilliseconds(serviceContext.SleepTime),
                        (exception, i) => HandleExceptionAsync(filters, exception, serviceContext, apiContext))
                    .ExecuteAsync(
                        ct => ExecuteApiMethod(filters, serviceContext, apiMethodFunc, apiContext),
                        (CancellationToken)apiContext);
            }
            catch (Exception ex) when (ex is OperationCanceledException || ex is TaskCanceledException)
            {
                throw ex;//new NoInternetConnectionException(ex);
            }
            catch (ApiMethodException ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public static void AddDefaultFilters(IServiceDecorator serviceDecorator)
        {
            //serviceDecorator.AddFilter<AuthServiceFilter>();
            //serviceDecorator.AddFilter<ApiMethodServiceFilter>();
            //serviceDecorator.AddFilter<CaptchaServiceFilter>();
        }

        private IList<IServiceFilter> GetServiceFilters(Type[] includeTypes = null, Type[] excludeTypes = null)
        {
            var filters = new List<IServiceFilter>();
            if (SingletonFilters.Count > 0)
            {
                filters.AddRange(SingletonFilters.Values.Select(x => x.Value));
            }

            if (includeTypes != null && includeTypes.Length > 0)
            {
                filters.AddRange(includeTypes.Select(CreateServiceFilter));
            }

            if (excludeTypes != null && excludeTypes.Length > 0)
            {
                filters.RemoveAll(x => excludeTypes.Any(y => x.GetType() == y));
            }

            return filters.OrderBy(x => x.Order).ToList();
        }

        private static async Task HandleExceptionAsync(
            IList<IServiceFilter> filters,
            Exception ex,
            ServiceContext serviceContext,
            ApiMethodContext apiContext)
        {
            ex.Data["serviceType"] = serviceContext.ServiceType.Name;
            await CheckTryResolveException(filters, ex, serviceContext, apiContext);
        }

        private static async Task<RequestResult<Response>> ExecuteApiMethod<Response>(
            IList<IServiceFilter> filters,
            ServiceContext serviceContext,
            Func<Task<Response>> apiMethodFunc,
            ApiMethodContext apiContext)
            where Response : class
        {
            await CheckDoRequest(filters, serviceContext, apiContext);
            RequestResult<Response> requestResult;
            try
            {
                var t = await apiMethodFunc();
                requestResult = new RequestResult<Response>(t, Enums.RequestStatus.Ok);
            }
            //catch (ValidationApiException e)
            //{
            //    requestResult = new RequestResult<Response>(e,Enums.RequestStatus.Ok);
            //}
            catch (Exception e)
            {
                throw new ApiMethodException(e);
            }

            await CheckValidateResult(filters, requestResult, apiContext);

            return requestResult;
        }

        private static T CreateServiceFilter<T>()
            where T : class, IServiceFilter
        {
            return Mvx.IoCProvider.IoCConstruct<T>();
        }

        private static IServiceFilter CreateServiceFilter(Type type)
        {
            return (IServiceFilter)Mvx.IoCProvider.IoCConstruct(type);
        }

        private static async Task CheckValidateResult<Response>(
            IList<IServiceFilter> filters,
            RequestResult<Response> requestResult,
            ApiMethodContext apiContext)
            where Response : class
        {
            if (filters.Count == 0)
            {
                return;
            }

            foreach (var filter in filters)
            {
                var filterResult = await filter.ValidateResult(requestResult, apiContext);
                if (filterResult.CanContinue)
                {
                    continue;
                }

                throw filterResult.Exception;
            }
        }

        private static async Task CheckDoRequest(IList<IServiceFilter> filters, ServiceContext serviceContext, ApiMethodContext apiContext)
        {
            if (filters.Count == 0)
            {
                return;
            }

            foreach (var filter in filters)
            {
                var filterResult = await filter.CanDoRequest(serviceContext, apiContext);
                if (filterResult.CanContinue)
                {
                    continue;
                }

                throw filterResult.Exception;
            }
        }

        private static async Task CheckTryResolveException(IList<IServiceFilter> filters, Exception ex, ServiceContext serviceContext,
            ApiMethodContext apiContext)
        {
            if (filters.Count == 0)
            {
                return;
            }

            foreach (var filter in filters)
            {
                var filterResult = await filter.TryResolveException(serviceContext, ex, apiContext);
                if (filterResult.CanContinue)
                {
                    continue;
                }

                throw filterResult.Exception;
            }
        }

        /// <summary>
        ///     Исключение об ошибке, выполненной во время вызова сервиса.
        /// </summary>
        private class ApiMethodException : Exception
        {
            /// <summary>
            ///     Создает новый экземпляр класса <see cref="ApiMethodException"/>.
            /// </summary>
            /// <param name="innerException">Исключение об ошибке.</param>
            public ApiMethodException(Exception innerException)
                : base(innerException?.Message, innerException)
            {
            }
        }
    }
}
