namespace Semdelion.DAL.Services.Filters
{
    using Semdelion.DAL.Models;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    ///     Фильтр сервиса.
    /// </summary>
    public interface IServiceFilter
    {
        /// <summary>
        ///     Приоритет по возрастанию.
        ///     Если не задан, то добавляется в конце.
        /// </summary>
        int? Order { get; }

        /// <summary>
        ///     Возможность сделать запрос на сервер.
        /// </summary>
        /// <param name="serviceContext">Контекст вызывающего сервиса.</param>
        /// <param name="apiContext">Контекст апи метода.</param>
        /// <returns>Результат валидации.</returns>
        Task<ServiceFilterResult> CanDoRequest(ServiceContext serviceContext, ApiMethodContext apiContext);

        /// <summary>
        ///     Валидировать результат.
        /// </summary>
        /// <param name="responseWrapper">Результат с сервера.</param>
        /// <param name="apiContext">Контекст апи метода.</param>
        /// <returns>Результат валидации.</returns>
        Task<ServiceFilterResult> ValidateResult<Response>(RequestResult<Response> requestResult, ApiMethodContext apiContext) where Response : class;

        /// <summary>
        ///     Может ли фильтр разрешить ошибку с сервера.
        /// </summary>
        /// <param name="serviceContext">Контекст вызывающего сервиса.</param>
        /// <param name="exception">Исключение об ошибке.</param>
        /// <param name="apiContext">Контекст апи метода.</param>
        /// <returns>Результат валидации.</returns>
        Task<ServiceFilterResult> TryResolveException(ServiceContext serviceContext, Exception exception, ApiMethodContext apiContext);
    }
}
