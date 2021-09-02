using NetTopologySuite.Geometries;

namespace AEShip.Service.Models
{
    public class Ship
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        /// <summary>
        /// Value in knot (nautical mile per hour)
        /// </summary>
        public double Velocity { get; set; }
    }
}
