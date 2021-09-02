namespace AEShip.Service.Models.Responses
{
    public class ClosestPortResponse
    {
        public string Id { get; }
        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double EstimatedArrivalTimeInHours { get; }

        public ClosestPortResponse(string id,
            string name,
            double latitude,
            double longitude,
            double estimatedArrivalTimeInHours)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            EstimatedArrivalTimeInHours = estimatedArrivalTimeInHours;
        }
    }
}
