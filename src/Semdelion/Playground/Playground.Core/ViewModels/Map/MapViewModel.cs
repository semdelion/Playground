using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;
using System.Collections.Generic;
using Playground.Core.ViewModels.Map.Item;
using System;

namespace Playground.Core.ViewModels.Map
{
    public class MapViewModel : BaseViewModel
    {
        public override string Title => "Map";

        public List<IMapItem> MapItems { get; set; } 
        
        public UserLocation CurrentUserLocation = new UserLocation { Latitude = 56.4977, Longitude = 84.9744 };

        public MapViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            MapItems = GetMapItems();
        }

        private List<IMapItem> GetMapItems()
        {
            MapItems = new List<IMapItem>();
            Random rnd = new Random();

            for (int i = 0; i < 50; ++i)
            {
                double theta = i * Math.PI * 0.33f;
                double radius = 0.005 * Math.Exp(0.1 * theta);
                double x = radius * Math.Cos(theta);
                double y = radius * Math.Sin(theta);
                MapItems.Add(new MapItem(CurrentUserLocation.Latitude + x, CurrentUserLocation.Longitude + y, $"star_{rnd.Next(1,500)}", rnd.Next(1,3) > 1 ? SpaceObjectType.Planet : SpaceObjectType.Star));
            }

            for (int i = 0; i < 50; ++i)
            {
                double theta = i * Math.PI * 0.33f;
                double radius = 0.005 * Math.Exp(0.1 * theta);
                double x = radius * Math.Cos(theta);
                double y = radius * Math.Sin(theta);
                MapItems.Add(new MapItem(CurrentUserLocation.Latitude - 1 + x, CurrentUserLocation.Longitude - 0.5 + y, $"star_{rnd.Next(1, 500)}", rnd.Next(1, 3) > 1 ? SpaceObjectType.Planet : SpaceObjectType.Star));
            }

            for (int i = 0; i < 50; ++i)
            {
                double theta = i * Math.PI * 0.33f;
                double radius = 0.005 * Math.Exp(0.1 * theta);
                double x = radius * Math.Cos(theta);
                double y = radius * Math.Sin(theta);
                MapItems.Add(new MapItem(CurrentUserLocation.Latitude + 0.6 + x, CurrentUserLocation.Longitude + 0.7 + y, $"star_{rnd.Next(1, 500)}", rnd.Next(1, 3) > 1 ? SpaceObjectType.Planet : SpaceObjectType.Star));
            }

            for (int i = 0; i < 50; ++i)
            {
                double theta = i * Math.PI * 0.33f;
                double radius = 0.005 * Math.Exp(0.1 * theta);
                double x = radius * Math.Cos(theta);
                double y = radius * Math.Sin(theta);
                MapItems.Add(new MapItem(CurrentUserLocation.Latitude - 0.7 + x, CurrentUserLocation.Longitude - 0.8 + y, $"star_{rnd.Next(1, 500)}", rnd.Next(1, 3) > 1 ? SpaceObjectType.Planet : SpaceObjectType.Star));
            }
            return MapItems;
        }

        public class UserLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }  
        }
    }
}
