using System;
using System.Collections.Generic;

namespace Bounce.Core
{
    internal class CollisionSolver
    {
        private readonly double _drawingAreaWidth;
        private readonly double _drawingAreaHeight;

        public CollisionSolver(double drawingAreaWidth, double drawingAreaHeight)
        {
            this._drawingAreaWidth = drawingAreaWidth;
            this._drawingAreaHeight = drawingAreaHeight;
        }

        public void SolveCollision(Ball a, Ball b)
        {
            double xDist = a.X - b.X;
            double yDist = a.Y - b.Y;
            double distSquared = Math.Pow(xDist, 2) + Math.Pow(yDist, 2);

            //Check the squared distances instead of the the distances, same result, but avoids a square root.
            if (distSquared <= (a.Radius + b.Radius) * (a.Radius + b.Radius))
            {
                double xVelocity = b.XVelocity - a.XVelocity;
                double yVelocity = b.YVelocity - a.YVelocity;
                double dotProduct = xDist * xVelocity + yDist * yVelocity;
                //Neat vector maths, used for checking if the objects moves towards one another.
                if (dotProduct > 0)
                {
                    double collisionScale = dotProduct / distSquared;
                    double xCollision = xDist * collisionScale;
                    double yCollision = yDist * collisionScale;
                    //The Collision vector is the speed difference projected on the Dist vector,
                    //thus it is the component of the speed difference needed for the collision.
                    double combinedMass = a.Mass + b.Mass;
                    double collisionWeightA = 2 * b.Mass / combinedMass;
                    double collisionWeightB = 2 * a.Mass / combinedMass;
                    a.XVelocity += collisionWeightA * xCollision;
                    a.YVelocity += collisionWeightA * yCollision;
                    b.XVelocity -= collisionWeightB * xCollision;
                    b.YVelocity -= collisionWeightB * yCollision;
                }
            }
        }

        public bool IsPositionOccupied(Ball b)
        {
            return false;
        }

        public void SolveBorderCollision(Ball b)
        {
            // Check for border collision
            if (b.X - b.Radius <= 0 ||
                b.X + b.Radius - this._drawingAreaWidth > 0)
            {
                b.XVelocity *= -1;
            }
            if (b.Y - b.Radius <= 0 ||
                b.Y + b.Radius - this._drawingAreaHeight > 0)
            {
                b.YVelocity *= -1;
            }
        }

        public bool IsPopulated(PointD position, double radius, ICollection<Ball> balls)
        {
            bool populated = false;
            foreach (Ball ball in balls)
            {
                if (Math.Abs(ball.X - position.X) < ball.Radius || Math.Abs(ball.Y - position.Y) < ball.Radius)
                {
                    populated = true;
                }
            }
            return populated;
        }
    }
}