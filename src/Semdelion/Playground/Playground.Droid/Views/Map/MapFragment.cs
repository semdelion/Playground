using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Map;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views.Map
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(MapFragment))]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback
    {
        private GoogleMap _map;

        private MapView _mapView;

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
            _map.AddMarker(new MarkerOptions().SetPosition(new LatLng(45.188529, 5.724523)).SetTitle("Mon marker"));
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