namespace Semdelion.DAL.Models
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    /// <summary>
    ///     АПИ контекст позволяет вызывать методы сервериса с различными параметрами.
    ///     Например, по умолчанию бизнес ошибки превращаются в ошибку <see cref="ApiBusinessResponseException" />.
    ///     Фатальные ошибки в <see cref="ApiFatalResponseException" /> и с отображением диалогового окна с ошибкой.
    ///     Чтобы вытащить <see cref="CancellationToken"/>, надо вызвать через неявное преобразование (CancellationToken)apiMethodContext.
    /// </summary>
    public class ApiMethodContext
    {
        public static Func<CancellationToken> CancellationTokenFunc = () => CancellationTokenSourceFunc().Token;

        public static Func<CancellationTokenSource> CancellationTokenSourceFunc = () => new CancellationTokenSource(TimeSpan.FromMinutes(2));

        /// <summary>
        ///     Конструктор апи контекста
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="throwResultModel">Делать обработку ошибок</param>
        /// <param name="handleFatalResult">Делать обработку фатальных ошибок</param>
        public ApiMethodContext(CancellationToken cancellationToken, bool throwResultModel = true, bool handleFatalResult = true)
        {
            CancellationToken = cancellationToken == default ? CancellationTokenFunc() : cancellationToken;
            ThrowResultModel = throwResultModel;
            HandleFatalResult = handleFatalResult;
        }

        /// <summary>Токен отмены</summary>
        private CancellationToken CancellationToken { get; }

        /// <summary>
        ///     Включить обработку запроса на сервисе.
        ///     по умолчанию включен.
        /// </summary>
        [DefaultValue(true)]
        public bool ThrowResultModel { get; }

        /// <summary>
        ///     Обработать фатальную ошибку на стороне сервиса
        ///     по умолчанию включен.
        /// </summary>
        [DefaultValue(true)]
        public bool HandleFatalResult { get; }

        /// <summary>
        ///     Колбек при вызове <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        internal CancellationTokenRegistration? CancellationCallback(Action callback)
        {
            if (CancellationToken != default)
            {
                return CancellationToken.Register(callback);
            }

            return null;
        }

        public static implicit operator ApiMethodContext(CancellationToken cancellationToken)
        {
            return new ApiMethodContext(cancellationToken);
        }

        public static explicit operator CancellationToken(ApiMethodContext context)
        {
            return context?.CancellationToken ?? CancellationTokenFunc();
        }

        /// <summary>
        ///     Результат запроса обрабатывает вызывающий код (Вью модель/Провайдер), а не сервис.
        /// </summary>
        /// <returns>апи контекст метода</returns>
        public static ApiMethodContext WithoutThrow()
        {
            return new ApiMethodContext(default, false);
        }

        /// <summary>
        ///     Указать кастомный токен отмены, а не брать по умолчанию с <see cref="CancellationTokenFunc" />
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="throwResultModel">Делать обработку ошибок</param>
        /// <returns>апи контекст метода</returns>
        public static ApiMethodContext WithCancellation(CancellationToken cancellationToken, bool throwResultModel = true)
        {
            return new ApiMethodContext(cancellationToken, throwResultModel);
        }

        /// <summary>
        ///     Дефолтный апи контекст с дефолтным токеном отмены и обработкой ошибки на сервисе.
        /// </summary>
        /// <returns>апи контекст метода</returns>
        public static ApiMethodContext Default => new ApiMethodContext(default);
    }
}
