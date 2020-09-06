using System;
using System.Collections.Generic;
using System.Text;

namespace Semdelion.Core.Helpers.Interfaces
{
    public interface IPlatformSpecificValue
    {
    }

    public interface IPlatformSpecificValue<out T> : IPlatformSpecificValue
    {
        new T Value { get; }
    }
}
