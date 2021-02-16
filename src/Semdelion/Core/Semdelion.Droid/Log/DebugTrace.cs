namespace Semdelion.Droid.Log
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using MvvmCross.Logging;
    using MvvmCross;
    using Semdelion.Core.Log;
    using Semdelion.Core.Extensions;

    public class DebugTrace : IMvxLog
    {
        private ILogWriter logWriter;
        protected ILogWriter LogWriter => Mvx.IoCProvider.CanResolve<ILogWriter>() ? (logWriter ??= Mvx.IoCProvider.Resolve<ILogWriter>()) : null;

        public bool Log(MvxLogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            try
            {
                var logEntry = string.Empty;

                if (messageFunc == null)
                {
                    logEntry = $"{logLevel}:" + (exception?.BuildAllMessagesAndStackTrace() ?? string.Empty);
                }
                else
                {
                    logEntry = formatParameters.Any()
                        ? string.Format(logLevel + ":" + messageFunc() + ":" + exception?.BuildAllMessagesAndStackTrace(), formatParameters)
                        : logLevel + ":" + messageFunc();
                }

                Debug.WriteLine(logEntry);

                LogWriter?.Write(logEntry);
            }
            catch (Exception)
            {
                this.Trace($"Exception during trace of {logLevel} {messageFunc?.Invoke()}");
            }
            return true;
        }

        public bool IsLogLevelEnabled(MvxLogLevel logLevel)
        {
            return true;
        }
    }
}