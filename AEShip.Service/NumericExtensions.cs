using System;

namespace AEShip.Service
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double value)
        {
            return (Math.PI / 180) * value;
        }
    }
}
