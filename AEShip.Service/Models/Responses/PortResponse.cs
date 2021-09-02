namespace AEShip.Service.Models.Responses
{
    public class PortResponse
    {
        public string Id { get; }
        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public PortResponse(string id,
            string name,
            double latitude,
            double longitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
