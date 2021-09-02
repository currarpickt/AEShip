using System;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;

namespace AEShip.Service.Services
{
    public class ShipUtilities: IShipUtilities
    {
        const double EarthRadiusKm = 6371;

        /// <summary>
        /// Using Haversine formula
        /// </summary>
        /// <param name="shipLocation">Ship location</param>
        /// <param name="portLocation">Port location</param>
        /// <returns></returns>
        public double GetDistance(GeoLocation shipLocation,
            GeoLocation portLocation)
        {
            var lat = (portLocation.Latitude - shipLocation.Latitude).ToRadians();
            var lng = (portLocation.Longitude - shipLocation.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(shipLocation.Latitude.ToRadians()) * Math.Cos(portLocation.Latitude.ToRadians()) *
                     Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return EarthRadiusKm * h2;
        }
    }
}
