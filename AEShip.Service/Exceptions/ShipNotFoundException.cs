using System;

namespace AEShip.Service.Exceptions
{
    public class ShipNotFoundException : Exception
    {
        public string Id { get; }

        public ShipNotFoundException() {}

        public ShipNotFoundException(string id)
            : base(CreateMessage(id))
        {
            Id = id;
        }
        
        public ShipNotFoundException(string id, Exception innerException)
            : base(CreateMessage(id), innerException)
        {
        }

        private static string CreateMessage(string id)
        {
            return $"No ship found with Id: {id}";
        }
    }
}
