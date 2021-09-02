namespace AEShip.Service.Models
{
    public readonly struct GeoLocation
    {
        public GeoLocation(double latitude,
            double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }
}
