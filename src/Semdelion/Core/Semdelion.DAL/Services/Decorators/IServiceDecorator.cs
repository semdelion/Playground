namespace Semdelion.DAL.Services.Decorators
{
    using Semdelion.DAL.Models;
    using Semdelion.DAL.Services.Filters;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Обертка для сервисов.
    /// </summary>
    public interface IServiceDecorator
    {
        /// <summary>
        ///     Список фильтров для сервисов.
        /// </summary>
        IList<IServiceFilter> Filters { get; }

        /// <summary>
        ///     Добавить фильтр.
        /// </summary>
        /// <typeparam name="TFilter">Тип добавляемого фильтра.</typeparam>
        void AddFilter<TFilter>() where TFilter : class, IServiceFilter;

        /// <summary>
        ///     Добавить фильтр.
        /// </summary>
        /// <param name="filterFunc">Делегат фильтра.</param>
        /// <typeparam name="TFilter">Тип добавляемого фильтра.</typeparam>
        void AddFilter<TFilter>(Func<TFilter> filterFunc) where TFilter : IServiceFilter;

        /// <summary>
        ///     Удалить фильтр.
        /// </summary>
        /// <typeparam name="TFilter">Тип удаляемого фильтра.</typeparam>
        void RemoveFilter<TFilter>() where TFilter : IServiceFilter;

        /// <summary>
        ///     Выполнить метод.
        /// </summary>
        /// <param name="serviceContext">Контекст сервиса.</param>
        /// <param name="apiFunc">Делегат выполняемого метода.</param>
        /// <param name="apiContext">Апи контекст.</param>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <returns>Результат выполненного метода.</returns>
        Task<RequestResult<TResult>> SendApiMethod<TResult>(
            ServiceContext serviceContext, 
            Func<Task<TResult>> apiFunc, 
            ApiMethodContext apiContext) where TResult : class;
    }
}
