using System;
using System.Text;

namespace Semdelion.Core.Extensions
{
    /// <summary>
    ///     Расширение для строкового типа данных.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///     Получить удобный для чтения стектрейс.
        /// </summary>
        /// <param name="exception">Ошибка.</param>
        /// <returns></returns>
        public static string BuildAllMessagesAndStackTrace(this Exception exception)
        {
            var innerException = exception;
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(exception.Message + " \n " + exception.StackTrace);
            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
                messageBuilder.AppendLine(innerException.Message + " \n " + innerException.StackTrace);
            }

            return messageBuilder.ToString();
        }
    }
}
