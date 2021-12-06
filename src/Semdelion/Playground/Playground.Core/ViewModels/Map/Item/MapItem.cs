namespace Playground.Core.ViewModels.Map.Item
{
    public class MapItem: IMapItem
    {
        public double Latitude { get; }

        public double Longitude { get;}

        public string Name { get; } 

        public SpaceObjectType SpaceObject { get; set; }

        public MapItem(double latitude, double longitude, string name)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
        }
        public MapItem(double latitude, double longitude, string name, SpaceObjectType spaceObject)
            : this(latitude, longitude, name) 
        {
            SpaceObject = spaceObject;
        }
    }

    public enum SpaceObjectType
    {
        Planet,
        Star
    }
}
