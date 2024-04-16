using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            // стороны треугольника ABX
            double axSide = Math.Abs(Math.Sqrt(((x - ax) * (x - ax)) + ((y - ay) * (y - ay))));
            double bxSide = Math.Abs(Math.Sqrt(((x - bx) * (x - bx)) + ((y - by) * (y - by))));
            double abSide = Math.Abs(Math.Sqrt(((ax - bx) * (ax - bx)) + ((ay - by) * (ay - by))));
            double mulScalarAXAB = (x - ax) * (bx - ax) + (y - ay) * (by - ay);
            double mulScalarBXAB = (x - bx) * (-bx + ax) + (y - by) * (-by + ay);
            if (abSide == 0) return axSide;
            else if (mulScalarAXAB >= 0 && mulScalarBXAB >= 0)
            {
                double p = (axSide + bxSide + abSide) / 2.0;
                double s = Math.Sqrt(Math.Abs((p * (p - axSide) * (p - bxSide) * (p - abSide))));
                return (2.0 * s) / abSide;
            }
            else if (mulScalarAXAB < 0 || mulScalarBXAB < 0)
            {
                return Math.Min(axSide, bxSide);
            }
            else return 0;
        }
    }
}