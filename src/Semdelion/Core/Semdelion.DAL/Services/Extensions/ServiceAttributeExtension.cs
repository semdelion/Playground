using Semdelion.DAL.Models;
using Semdelion.DAL.Services.Attributes;
using System;

namespace Semdelion.DAL.Services.Extensions
{
    /// <summary>
    ///     Расширения для типа <see cref="ServiceAttribute"/>.
    /// </summary>
    public static class ServiceAttributeExtension
    {
        /// <summary>
        ///     Получить контекст сервиса по типу сервиса.
        /// </summary>
        /// <param name="serviceAttribute">Атрибут сервиса.</param>
        /// <param name="serviceType">Тип сервиса.</param>
        /// <returns>Контекст сервиса.</returns>
        public static ServiceContext GetServiceContext(this ServiceAttribute serviceAttribute, Type serviceType)
        {
            return new ServiceContext(serviceType)
            {
                CheckAuth = serviceAttribute.CheckAuth,
                MaxRetryCount = serviceAttribute.MaxRetryCount,
                IncludeFilters = serviceAttribute.IncludeFilters,
                ExcludeFilters = serviceAttribute.ExcludeFilters
            };
        }
    }
}
