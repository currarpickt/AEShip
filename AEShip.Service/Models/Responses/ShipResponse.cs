namespace AEShip.Service.Models.Responses
{
    public class ShipResponse
    {
        public string Id { get; }
        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double Velocity { get; }

        public ShipResponse(string id,
            string name,
            double latitude,
            double longitude,
            double velocity)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Velocity = velocity;
        }
    }
}
