using Android.Gms.Maps.Model;
using Android.Gms.Maps.Utils.Clustering;
using Android.Runtime;
using Playground.Core.ViewModels.Map.Item;
using System;

namespace Playground.Droid.Views.Map
{
    public class ClusterItem : Java.Lang.Object, IClusterItem
    {
        public LatLng Position { get; set; }
        public string Snippet { get; set; }
        public string Title { get; set; }
        public SpaceObjectType SpaceObject { get; set; }

        public ClusterItem(double lat, double lon, string name, SpaceObjectType spaceObject)
        {
            Position = new LatLng(lat, lon);
            Title = name;
            SpaceObject = spaceObject;
        }

        public ClusterItem(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer)
        {
        }
    }
}