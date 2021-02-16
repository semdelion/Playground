namespace Semdelion.iOS.Log
{
    using MvvmCross.Logging;
    using System;

    public class LogProvider : IMvxLogProvider
    {
        private DebugTrace _trace;

        protected delegate IDisposable OpenNdc(string message);
        protected delegate IDisposable OpenMdc(string key, string value);

        private readonly Lazy<OpenNdc> _lazyOpenNdcMethod;
        private readonly Lazy<OpenMdc> _lazyOpenMdcMethod;

        private static readonly IDisposable NoopDisposableInstance = new DisposableAction();

        public LogProvider()
        {
            _lazyOpenNdcMethod
                = new Lazy<OpenNdc>(GetOpenNdcMethod);

            _lazyOpenMdcMethod
               = new Lazy<OpenMdc>(GetOpenMdcMethod);
        }

        public IMvxLog GetLogFor<T>()
        {
            return _trace ??= new DebugTrace();
        }

        public IMvxLog GetLogFor(string name)
        {
            return _trace ??= new DebugTrace();
        }

        public IDisposable OpenNestedContext(string message)
            => _lazyOpenNdcMethod.Value(message);

        public IDisposable OpenMappedContext(string key, string value)
            => _lazyOpenMdcMethod.Value(key, value);

        protected virtual OpenNdc GetOpenNdcMethod()
            => _ => NoopDisposableInstance;

        protected virtual OpenMdc GetOpenMdcMethod()
            => (_, __) => NoopDisposableInstance;

        public IMvxLog GetLogFor(Type type)
        {
            throw new NotImplementedException();
        }
    }

    internal class DisposableAction : IDisposable
    {
        private readonly Action _onDispose;

        public DisposableAction(Action onDispose = null)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (_onDispose == null) return;
            _onDispose();
        }
    }
}