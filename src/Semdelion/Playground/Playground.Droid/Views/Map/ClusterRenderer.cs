using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Maps.Utils.Clustering;
using Android.Gms.Maps.Utils.Clustering.View;
using Android.Gms.Maps.Utils.UI;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Playground.Core.ViewModels.Map.Item;
using Semdelion.Droid.Extensions;
using System.Collections.Generic;

namespace Playground.Droid.Views.Map
{
    public class ClusterRenderer : DefaultClusterRenderer
    {
        private IconGenerator _iconGeneratorForMarkerGroup;
        private ImageView _imageviewForMarkerGroup;
        private Activity _context;
        public Dictionary<string, ClusterItem> _dicAllMarkerOnMap;

        public ClusterRenderer(Activity context, GoogleMap map, ClusterManager clusterManager, Dictionary<string, ClusterItem> dicAllMarkerOnMap) : base(context, map, clusterManager)
        {
            InitViewForMarkerGroup(context);
            _context = context;
            _dicAllMarkerOnMap = dicAllMarkerOnMap;
        }

        private void InitViewForMarkerGroup(Activity context)
        {
            //Retrieve views from AXML to display groups of markers (clustering)
            View viewMarkerClusterGrouped = context.LayoutInflater.Inflate(Resource.Layout._template_galaxy_cluster, null);
            _imageviewForMarkerGroup = viewMarkerClusterGrouped.FindViewById<ImageView>(Resource.Id.marker_cluster_grouped_imageview);

            //Configure the groups of markers icon generator with the view. The icon generator will be used to display the marker's picture with a text
            _iconGeneratorForMarkerGroup = new IconGenerator(context);
            _iconGeneratorForMarkerGroup.SetContentView(viewMarkerClusterGrouped);
            _iconGeneratorForMarkerGroup.SetBackground(null);
        }

        protected override void OnClusterItemRendered(Java.Lang.Object item, Marker marker)
        {
            base.OnClusterItemRendered(item, marker);

            ClusterItem clusterItem = (ClusterItem)item;

            if (!_dicAllMarkerOnMap.ContainsKey(marker.Id))
                _dicAllMarkerOnMap.Add(marker.Id, clusterItem);
        }

        protected override void OnBeforeClusterItemRendered(Java.Lang.Object p0, MarkerOptions markerOptions)
        {
            ClusterItem clusterItem = (ClusterItem)p0;
            Bitmap bitmap = null;

            if (clusterItem.SpaceObject == SpaceObjectType.Star)
            {
                bitmap = BitmapFactory.DecodeResource(_context.Resources, Resource.Drawable.ic_sun);
                
            }
            if (clusterItem.SpaceObject == SpaceObjectType.Planet)
            {
                bitmap = BitmapFactory.DecodeResource(_context.Resources, Resource.Drawable.ic_earth);
            }

            Bitmap smallMarker = Bitmap.CreateScaledBitmap(bitmap, (int)Converters.DpToPx(_context, 28), (int)Converters.DpToPx(_context, 28), false);
            //Icon for single marker
            markerOptions.SetIcon(BitmapDescriptorFactory.FromBitmap(smallMarker));

            //Text for Info Window
            markerOptions.SetTitle(clusterItem.Title);
            markerOptions.SetSnippet(clusterItem.Snippet);
        }


        protected override void OnBeforeClusterRendered(ICluster p0, MarkerOptions markerOptions)
        {
            string numberOfMarkersGrouped = p0.Size.ToString();

            bool isPlanet = false;
            foreach (ClusterItem itm in p0.Items)
            {
                isPlanet = itm.SpaceObject == SpaceObjectType.Planet;
                break;
            }

            _imageviewForMarkerGroup.SetImageResource(isPlanet ? Resource.Drawable.ic_earth_galaxy : Resource.Drawable.ic_galaxy);
            Bitmap icon = _iconGeneratorForMarkerGroup.MakeIcon(numberOfMarkersGrouped);
            markerOptions.SetIcon(BitmapDescriptorFactory.FromBitmap(icon));
        }
    }
}