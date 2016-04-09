using System;

namespace Bounce.Core
{
    internal static class Utility
    {
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}