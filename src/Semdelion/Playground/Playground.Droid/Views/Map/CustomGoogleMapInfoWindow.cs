using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.Droid.Views.Map
{
    public class CustomGoogleMapInfoWindow : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
    {
        private Activity _context;
        private View _view;
        private Marker _currentMarker;
        private Dictionary<string, ClusterItem> _dicAllMarkerOnMap;

        public CustomGoogleMapInfoWindow(Activity context, Dictionary<string, ClusterItem> dicAllMarkerOnMap)
        {
            _context = context;
            _view = _context.LayoutInflater.Inflate(Resource.Layout._template_map_info_window, null);
            _dicAllMarkerOnMap = dicAllMarkerOnMap;
        }

        public View GetInfoWindow(Marker marker)
        {
            //Use the default info window
            return null;
        }

        public View GetInfoContents(Marker marker)
        {
            if (marker == null)
                return null;

            _currentMarker = marker;

            ImageView imageview = _view.FindViewById<ImageView>(Resource.Id._template_map_info_window_imageview);
            TextView textviewTitle = _view.FindViewById<TextView>(Resource.Id._template_map_info_window_textview_title);
            TextView textviewDescription = _view.FindViewById<TextView>(Resource.Id._template_map_info_window_textview_description);

            ClusterItem clusterItem = null;
            _dicAllMarkerOnMap.TryGetValue(_currentMarker.Id, out clusterItem);

            //Add the custom information to the info window
            if (clusterItem != null)
                textviewTitle.Text += clusterItem.Title;

            textviewDescription.Text = marker.Snippet;


            return _view;
        }
    }
}