namespace Semdelion.Core.Log
{
    using System;

    public interface ILogWriter
    {
        event EventHandler OnWritten;

        void Write(string entry);
    }
}
