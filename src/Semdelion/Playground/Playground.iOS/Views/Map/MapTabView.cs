using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Google.Maps;
using MapKit;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Map;
using Semdelion.iOS.Views.Base;

namespace Playground.iOS.Views.Map
{
    [MvxTabPresentation(TabName = "Map", TabIconName = "ic_map", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class MapTabView : BaseViewController<MapViewModel>
    {
        private CLLocationManager _locationManager = new CLLocationManager();

        private List<CLLocationCoordinate2D> Coordinates;

        protected override void ConfigureViews()
        {
            base.ConfigureViews();

            CheckLocationAuthorization();

            Coordinates = new List<CLLocationCoordinate2D>()
            {
                new CLLocationCoordinate2D(42.364260, -71.120824),
                new CLLocationCoordinate2D(30.2649977168594, -97.73863627705),
                new CLLocationCoordinate2D(43.690854, -79.391111),
                new CLLocationCoordinate2D(43.736333, -107.272647)
            };

            var minLatitude = Coordinates.Min(x => x.Latitude);
            var maxLatitude = Coordinates.Max(x => x.Latitude);
            var minLongitude = Coordinates.Min(x => x.Longitude);
            var maxLongitude = Coordinates.Max(x => x.Longitude);

            var zoomLatitude = (minLatitude + maxLatitude) / 2;
            var zoomLongitude = (minLongitude + maxLongitude) / 2;

            SetupMKMapView(zoomLatitude, zoomLongitude, maxLatitude - minLatitude, maxLongitude - minLongitude);
            SetupGoogleMap(zoomLatitude, zoomLongitude);
        }

        private void CheckLocationAuthorization()
        {
            if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                MapView.ShowsUserLocation = true;
                GoogleMapView.MyLocationEnabled = true;
                GoogleMapView.Settings.MyLocationButton = true;
            }
            else
                _locationManager.RequestWhenInUseAuthorization();
        }

        private void SetupMKMapView(double zoomLatitude, double zoomLongitude, double maxminLatitude, double maxminLongitude)
        {
            LocationButton.TouchUpInside += (sender, e) =>
            {
                var userRegion = new MKCoordinateRegion(MapView.UserLocation.Coordinate, new MKCoordinateSpan(0.2, 0.2));
                MapView.SetRegion(userRegion, true);
            };

            foreach (var item in Coordinates)
                MapView.AddAnnotations(new MKPointAnnotation() { Coordinate = item });

            MapView.SetRegion(new MKCoordinateRegion(
                new CLLocationCoordinate2D(zoomLatitude, zoomLongitude),
                new MKCoordinateSpan(maxminLatitude * 2, maxminLongitude * 2)), true);
        }

        private void SetupGoogleMap(double zoomLatitude, double zoomLongitude)
        {
            GoogleMapView.Camera = CameraPosition.FromCamera(zoomLatitude, zoomLongitude, 3f);
            foreach (var item in Coordinates)
            {
                new Marker()
                {
                    Position = item,
                    Map = GoogleMapView
                };
            }
        }
    }   
}

