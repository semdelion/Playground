// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Playground.iOS.Views.Map
{
    [Register ("MapTabView")]
    partial class MapTabView
    {
        [Outlet]
        Google.Maps.MapView GoogleMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LocationButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LocationButton != null) {
                LocationButton.Dispose ();
                LocationButton = null;
            }

            if (MapView != null) {
                MapView.Dispose ();
                MapView = null;
            }
        }
    }
}