namespace Semdelion.DAL.Services.Attributes
{
    using System;

    /// <summary>
    ///     Атрибут для сервиса, который позволяет не прокидывать во все методы <see cref="ServiceContext" />,
    ///     а формировать его из настроек этого атрибута. <see cref="BaseService" /> управляет конвертацией атрибута в контекст
    ///     сервиса. Этот атрибут удобно прописывать в интерфейсах сервисов,
    ///     тк в основном мы работаем с контрактом, нежели с реализацией контракта.
    ///     Допустима навешивать атрибут на класс, интерфейс, метод.
    ///     Если установлен на классе и методе, то атрибут берется сначала с метода.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class ServiceAttribute : Attribute
    {
        public const int NoRetryCount = 0;
        private const int DefaultMaxRetryCount = 1;
        private const int DefaultSleepTime = 200;

        /// <summary>
        ///     Проверять ли авторизованность. Этот параметр чисто информативный, те вся ответственность за проверку уходит в фильтры.
        ///     В фильтрах необходимо проверять значение этого параметра.
        /// </summary>
        public bool CheckAuth { get; set; }

        /// <summary>
        ///     Количество повторов на сервер, если вдруг произошла ошибка.
        ///     По умолчанию = 1.
        /// </summary>
        public int MaxRetryCount { get; set; } = DefaultMaxRetryCount;

        /// <summary>
        ///     Время ожидания между запросами 
        ///     По умолчанию = 200.
        /// </summary>
        public int SleepTime { get; set; } = DefaultSleepTime;

        /// <summary>
        ///     Добавочные фильтры к вызову метода сервиса.
        /// </summary>
        public Type[] IncludeFilters { get; set; }

        /// <summary>
        ///     Исключаемые фильтры к вызову метода сервиса.
        /// </summary>
        public Type[] ExcludeFilters { get; set; }

        /// <summary>
        ///     Наличие добавочных фильтров.
        /// </summary>
        public bool HasIncludeFilters => IncludeFilters != null && IncludeFilters.Length > 0;

        /// <summary>
        ///     Наличие исключаемых фильтров.
        /// </summary>
        public bool HasExcludeFilters => ExcludeFilters != null && ExcludeFilters.Length > 0;
    }
}
