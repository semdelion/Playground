using System;
using System.Collections.Generic;
using System.Text;

namespace Playground.Core.ViewModels.Map.Item
{
    public interface IMapItem
    {
        double Latitude { get; }
        double Longitude { get; }
        string Name { get; }
    }
}
