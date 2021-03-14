namespace Semdelion.Core.Log
{
    using System.Collections.Generic;
    using System;

    public interface ILogReader
    {
        event EventHandler<string> OnReaded;

        void Read();

        IEnumerable<string> ReadAll();
    }
}
