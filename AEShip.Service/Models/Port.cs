using NetTopologySuite.Geometries;

namespace AEShip.Service.Models
{
    public class Port
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }

    public class ClosestPort
    {
        public Port Port { get; }
        public double Distance { get; }

        public ClosestPort(Port port, double distance)
        {
            Port = port;
            Distance = distance;
        }
    }
}
