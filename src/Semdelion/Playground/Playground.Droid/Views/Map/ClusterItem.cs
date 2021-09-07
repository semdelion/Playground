using Android.Gms.Maps.Model;
using Android.Gms.Maps.Utils.Clustering;
using Android.Runtime;
using System;

namespace Playground.Droid.Views.Map
{
    public class ClusterItem : Java.Lang.Object, IClusterItem
    {
        public LatLng Position { get; set; }
        public string Snippet { get; set; }
        public string Title { get; set; }

        public ClusterItem(double lat, double lon)
        {
            Position = new LatLng(lat, lon);
            Title = lat.ToString() + ", " + lon.ToString();
        }

        public ClusterItem(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer)
        {
        }
    }
}