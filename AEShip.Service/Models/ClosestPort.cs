namespace AEShip.Service.Models
{
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
