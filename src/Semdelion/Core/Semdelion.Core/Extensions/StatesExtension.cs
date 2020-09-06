namespace Semdelion.Core.Extensions
{
    using Refit;
    using Semdelion.Core.Enums;
    using Semdelion.Core.ViewModels.Interfaces;
    using Semdelion.DAL;
    using Semdelion.DAL.Exceptions;
    using System;

    /// <summary>
    ///     Расширение для типа <see cref="States"/>
    /// </summary>
    public static class StatesExtension
    {
        public static void UpdateState(this IBaseViewModel vm, RequestResult requestResult)
        {
            if (requestResult.IsValid)
            {
                vm.State = States.Normal;
            }
            else if (requestResult.Exception is NetworkConnectionException)
            {
                vm.State = States.NoInternet;
            }
            else if (requestResult.Exception is ApiException || requestResult.Exception is Exception)
            {
                vm.State = States.Error;
                vm.StateMessage = requestResult.Message;
            }
            else
            {
                vm.State = States.NoData;
            }
        }
    }
}
