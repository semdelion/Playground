namespace Semdelion.Core.Extensions
{
    using Refit;
    using Semdelion.Core.Enums;
    using Semdelion.DAL.Exceptions;
    using System;

    /// <summary>
    ///     Расширение для типа <see cref="States"/>
    /// </summary>
    public static class StatesExtension
    {
        public static States GetState(Exception ex)
        {
            if (ex is NetworkConnectionException)
                return States.NoInternet;
            if (ex is ApiException || ex is Exception)
                return States.Error;

            return States.NoData;
        }
    }
}
