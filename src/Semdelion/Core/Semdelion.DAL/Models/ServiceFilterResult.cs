namespace Semdelion.DAL.Models
{
    using System;

    public class ServiceFilterResult
    {
        private ServiceFilterResult(bool canContinue) => CanContinue = canContinue;

        private ServiceFilterResult(Exception exception) => Exception = exception;

        public bool CanContinue { get; }

        public Exception Exception { get; }

        public static ServiceFilterResult Skip => new ServiceFilterResult(true);

        public static ServiceFilterResult Error(Exception exception) => new ServiceFilterResult(exception);
    }
}
