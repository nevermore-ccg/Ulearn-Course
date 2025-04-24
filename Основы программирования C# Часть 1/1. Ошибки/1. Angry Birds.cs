using System;

namespace AngryBirds
{
    public static class AngryBirdsTask
    {
        const double G = 9.8;
        public static double FindSightAngle(double v, double distance)
        {
            return 0.5 * Math.Asin((G * distance) / (v * v));
        }
    }
}