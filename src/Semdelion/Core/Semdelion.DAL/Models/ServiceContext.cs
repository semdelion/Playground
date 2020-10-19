namespace Semdelion.DAL.Models
{
    using Semdelion.DAL.Services.Attributes;
    using System;

    /// <summary>
    ///     Контекст сервиса позволяет настраивать вызовы методов сервиса с уже преднастроенными параметрами:
    ///     - делать ли проверку авторизации;
    ///     - включать/исключать различные фильтры.
    ///     Контекст сервиса автоматически формируется в <see cref="BaseService" />.
    ///     По умолчанию проверка авторизации включена и нет включенных/исключенных фильтров.
    /// </summary>
    public sealed class ServiceContext : ServiceAttribute
    {
        /// <summary>
        ///     Создает новый экземпляр класса <see cref="ServiceContext"/>.
        /// </summary>
        /// <param name="serviceType">Тип вызывающего сервиса.</param>
        public ServiceContext(Type serviceType)
        {
            ServiceType = serviceType;
        }

        /// <summary>
        ///     Тип вызывающего сервиса.
        /// </summary>
        public Type ServiceType { get; }
    }
}
