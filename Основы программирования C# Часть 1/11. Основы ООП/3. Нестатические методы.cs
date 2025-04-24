namespace Geometry
{
    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt(Math.Pow(segment.End.X - segment.Begin.X, 2)
                + Math.Pow(segment.End.Y - segment.Begin.Y, 2));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var lengthSegment = GetLength(segment);
            var length1 = GetLength(new Segment { Begin = segment.Begin, End = vector });
            var length2 = GetLength(new Segment { Begin = vector, End = segment.End });
            return Equals(lengthSegment, length1 + length2);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector
            {
                X = vector1.X + vector2.X,
                Y = vector1.Y + vector2.Y,
            };
        }
    }
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength() => Geometry.GetLength(this);

        public Vector Add(Vector vector) => Geometry.Add(this, vector);

        public bool Belongs(Segment segment) => Geometry.IsVectorInSegment(this, segment);
    }
    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength() => Geometry.GetLength(this);

        public bool Contains(Vector vector) => Geometry.IsVectorInSegment(vector, this);
    }
}