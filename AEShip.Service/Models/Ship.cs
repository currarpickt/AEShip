namespace AEShip.Service.Models
{
    public class Ship
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// Value in km/hour
        /// </summary>
        public double Velocity { get; set; }
    }
}
