using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Maps.Utils.Clustering;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Map;
using Playground.Core.ViewModels.Map.Item;
using Semdelion.Droid.Views;
using System.Collections.Generic;
using static Android.Gms.Maps.GoogleMap;
/// <summary>
/// read this
/// https://www.maximeesprit.com/en/xamarin-android-grouping-google-map-markers-with-clustering/
/// </summary>

namespace Playground.Droid.Views.Map
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(MapFragment))]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, ClusterManager.IOnClusterItemInfoWindowClickListener, IOnCameraIdleListener, IInfoWindowAdapter, IOnInfoWindowClickListener
    {
        private GoogleMap _map;

        private MapView _mapView;

        private ClusterManager _clusterManagerStar;
        private ClusterRenderer _clusterRendererStar;

        private ClusterManager _clusterManagerPlanet;
        private ClusterRenderer _clusterRendererPlanet;

        public Dictionary<string, ClusterItem> _dicAllMarkerOnMap = new Dictionary<string, ClusterItem>();

        CustomGoogleMapInfoWindow _infoWindowAdapter;


        protected override int FragmentId => Resource.Layout.map_view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _mapView = view.FindViewById<MapView>(Resource.Id.google_map);

            _mapView.OnCreate(savedInstanceState);
            _mapView.GetMapAsync(this);

            return view;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            //_map.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(this, Resource.Raw.map_style_night)); TODO относительно темы
            _map.SetOnCameraIdleListener(this);
            _map.SetInfoWindowAdapter(this);
            _map.SetOnInfoWindowClickListener(this);

            _infoWindowAdapter = new CustomGoogleMapInfoWindow(Activity, _dicAllMarkerOnMap);

            ConfigClusterStars();

            ConfigClusterPlanets();

            SetupMap();
        }

        private void ConfigClusterStars()
        {
            //Initialize cluster manager.
            _clusterManagerStar = new ClusterManager(Context, _map);

            //Initialize cluster renderer, and keep a reference that will be usefull for the InfoWindowsAdapter
            _clusterRendererStar = new ClusterRenderer(Activity, _map, _clusterManagerStar, _dicAllMarkerOnMap);
            _clusterManagerStar.Renderer = _clusterRendererStar;

            //Custom info window : single markers only (a click on a cluster marker should not show info window)
            _clusterManagerStar.MarkerCollection.SetOnInfoWindowAdapter(_infoWindowAdapter);

            //Handle Info Window's click event
            _clusterManagerStar.SetOnClusterItemInfoWindowClickListener(this);
        }

        private void ConfigClusterPlanets()
        {
            //Initialize cluster manager for favorites markers.
            _clusterManagerPlanet = new ClusterManager(Context, _map);

            //Initialize cluster renderer, and keep a reference that will be usefull for the InfoWindowsAdapter
            _clusterRendererPlanet = new ClusterRenderer(Activity, _map, _clusterManagerPlanet, _dicAllMarkerOnMap);
            _clusterManagerPlanet.Renderer = _clusterRendererPlanet;

            //Custom info window : single markers only (a click on a cluster marker should not show info window)
            _clusterManagerPlanet.MarkerCollection.SetOnInfoWindowAdapter(_infoWindowAdapter);

            //Handle Info Window's click event
            _clusterManagerPlanet.SetOnClusterItemInfoWindowClickListener(this);
        }


        public void OnClusterItemInfoWindowClick(Java.Lang.Object p0)
        {
            //You can retrieve the ClusterItem clicked with a cast
            ClusterItem itemClicked = (ClusterItem)p0;

            //Dismiss the info window clicked
            Marker markerClicked = _clusterRendererStar.GetMarker(itemClicked) ?? _clusterRendererPlanet.GetMarker(itemClicked);

            markerClicked.HideInfoWindow();
        }

        public void OnCameraIdle()
        {
            _clusterManagerStar.OnCameraIdle();
            _clusterManagerPlanet.OnCameraIdle();
        }

        public View GetInfoContents(Marker marker)
        {
            return _clusterManagerStar.MarkerManager.GetInfoContents(marker) ?? 
                _clusterManagerPlanet.MarkerManager.GetInfoContents(marker);
        }

        public View GetInfoWindow(Marker marker)
        {
            return _clusterManagerStar.MarkerManager.GetInfoWindow(marker) ?? 
                _clusterManagerPlanet.MarkerManager.GetInfoWindow(marker);
        }

        public void OnInfoWindowClick(Marker marker)
        {
            _clusterManagerStar.OnInfoWindowClick(marker);
            _clusterManagerPlanet.OnInfoWindowClick(marker);
        }

        private void SetupMap()
        {
            LatLng LatLonGrenoble = new LatLng(ViewModel.CurrentUserLocation.Latitude, ViewModel.CurrentUserLocation.Longitude);
            _map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(LatLonGrenoble, 12));

            List<ClusterItem> starMarkers = new List<ClusterItem>();
            List<ClusterItem> planetMarkers = new List<ClusterItem>();

            foreach (var item in ViewModel.MapItems)
            {
                MapItem mapItem = item as MapItem;
               
                if (mapItem.SpaceObject == SpaceObjectType.Star)
                    starMarkers.Add(new ClusterItem(mapItem.Latitude, mapItem.Longitude, mapItem.Name, mapItem.SpaceObject));
                else
                    planetMarkers.Add(new ClusterItem(mapItem.Latitude, mapItem.Longitude, mapItem.Name, mapItem.SpaceObject));
            }

            _clusterManagerStar.AddItems(starMarkers);
            _clusterManagerPlanet.AddItems(planetMarkers);
        }

        public override void OnPause()
        {
            base.OnPause();
            _mapView?.OnPause();
        }

        public override void OnResume()
        {
            base.OnResume();
            _mapView?.OnResume();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _mapView?.OnSaveInstanceState(outState);
        }
    }
}