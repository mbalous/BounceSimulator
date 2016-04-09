using System.Windows.Controls;
using Bounce.Core;

namespace BounceWPF
{
    internal class CanvasDrawingArea : DrawingArea
    {
        public override double Height => this.CanvasArea.ActualHeight;

        public override double Width => this.CanvasArea.ActualWidth;

        public Canvas CanvasArea { get; set; }

        public CanvasDrawingArea(Canvas canvas)
        {
            this.CanvasArea = canvas;
        }

        public override void UpdatePosition(Ball ball)
        {
            BallWpf castedBall = (BallWpf) ball;
            Canvas.SetLeft(castedBall.WpfShape, castedBall.X - castedBall.Radius);
            Canvas.SetTop(castedBall.WpfShape, castedBall.Y - castedBall.Radius);
        }
    }
}