using System.Windows.Media;
using System.Windows.Shapes;
using Bounce.Core;

namespace BounceWPF
{
    internal class BallWpf : Ball
    {
        public Ellipse WpfShape { get; }

        public BallWpf(PointD position, double xVelocity, double yVelocity, double radius, Brush fill) : base(position, radius, xVelocity, yVelocity)
        {
            this.WpfShape = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = fill
            };
        }
    }
}