namespace AEShip.Service.Models.Requests
{
    public class UpdateVelocityRequest
    {
        public string ShipId { get; set; }
        public double Velocity { get; set; }
    }
}
