
namespace Semdelion.DAL.Exceptions
{
    using System;

    /// <summary>
    /// 	Исключение о нестабильном интернет соединении.
    /// </summary>
    public class NetworkConnectionException : Exception
    {
        /// <summary>
        /// 	Создает новый экземпляр класса <see cref="NetworkConnectionException"/>
        /// </summary>
        public NetworkConnectionException()
        {
        }

        /// <summary>
	    /// 	Создает новый экземпляр класса <see cref="NetworkConnectionException"/> с сообщением об ошибке.
	    /// </summary>
	    /// <param name="message">Сообщение об ошибке.</param>
        public NetworkConnectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 	Создает новый экземпляр класса <see cref="NetworkConnectionException"/> с сообщением и исключением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="innerException">Исключение об ошибке.</param>
        public NetworkConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
