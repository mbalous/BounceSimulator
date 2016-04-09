using System.Drawing;

#pragma warning disable 1591

namespace Bounce.Core
{
    /// <summary>
    /// Represents an ordered pair of double precision floating points x- and y-coordinates that defines a point in a
    /// two-dimensional plane.
    /// For further reference look into <see cref="P:System.Drawing.Point" />
    /// </summary>
    public struct PointD
    {
        public double X;
        public double Y;

        public PointD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point ToPoint()
        {
            return new Point((int) this.X, (int) this.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is PointD && this == (PointD) obj;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static bool operator ==(PointD a, PointD b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PointD a, PointD b)
        {
            return !(a == b);
        }
    }
}
