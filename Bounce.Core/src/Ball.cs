namespace Bounce.Core
{
    /// <summary>
    /// Class representing the ball object.
    /// You must provide your own implementation.
    /// </summary>
    public abstract class Ball
    {
        /// <summary>
        /// Position on the x axis (horizontal).
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Position on the y axis (vertical).
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Radius of the ball.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// Mass of the ball, it is calculated from the radius.
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Velocity in the horizontal direction
        /// </summary>
        public double XVelocity { get; set; }

        /// <summary>
        /// Velocity in the vertical direction.
        /// </summary>
        public double YVelocity { get; set; }


        /// <summary>
        /// Ball constructor.
        /// </summary>
        /// <param name="position">Initial position of the ball.</param>
        /// <param name="radius">Radius of the ball.</param>
        /// <param name="xVelocity">Initial velocity on the x axis (horizontal).</param>
        /// <param name="yVelocity">Initial velocity on the y axis (vertical).</param>
        protected Ball(PointD position, double radius, double xVelocity, double yVelocity)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Radius = radius;
            this.Mass = radius*0.5;
            this.XVelocity = xVelocity;
            this.YVelocity = yVelocity;
        }
    }
}