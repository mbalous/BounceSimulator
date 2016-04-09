using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Bounce.Core
{
    /// <summary>
    /// Class representing the world.
    /// </summary>
    public class World
    {
        private readonly ObservableCollection<Ball> _balls;
        /// <summary>
        /// Invoked when an object is added or removed from the collection with game objects.
        /// </summary>
        public NotifyCollectionChangedEventHandler CollectionChangedHandler;
        private readonly DrawingArea _drawingArea;
        private readonly CollisionSolver _collisionSolver;
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Creates new world.
        /// </summary>
        /// <param name="drawingArea">Drawing area.</param>
        public World(DrawingArea drawingArea)
        {
            this._balls = new ObservableCollection<Ball>();
            this._balls.CollectionChanged += (sender, args) => this.CollectionChangedHandler.Invoke(sender, args);
            this._drawingArea = drawingArea;
            this._collisionSolver = new CollisionSolver(this._drawingArea.Width, this._drawingArea.Height);
        }

        public void AddObject(Ball obj)
        {
            if (this._collisionSolver.IsPositionOccupied(obj))
            {
                throw new ArgumentException("Position is already ocupied.");
            }
            this._balls.Add(obj);
        }


        public PointD GetUnoccupiedPosition(double radius)
        {
            double x = Rnd.Next((int) (radius + 1), (int) (this._drawingArea.Width - radius));
            double y = Rnd.Next((int) (radius + 1), (int) (this._drawingArea.Height - radius));

            if (this._balls.Count == 0)
            {
                return new PointD(x, y);
            }
            while (this._collisionSolver.IsPopulated(new PointD(x, y), radius, this._balls))
            {
                x = Rnd.Next((int) (radius + 1), (int) (this._drawingArea.Width - radius));
                y = Rnd.Next((int) (radius + 1), (int) (this._drawingArea.Height - radius));
            }
            return new PointD(x, y);
        }

        public void OnTick()
        {
            HandleCollisions();
            foreach (Ball o in this._balls)
            {
                Move(o);
            }
        }

        private void Move(Ball o)
        {
            o.X += o.XVelocity;
            o.Y += o.YVelocity;
            this._drawingArea.UpdatePosition(o);
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < this._balls.Count; i++)
            {
                Ball a = this._balls[i];
                this._collisionSolver.SolveBorderCollision(a);
                for (int j = i + 1; j < this._balls.Count; j++)
                {
                    Ball b = this._balls[j];
                    this._collisionSolver.SolveCollision(a, b);
                }
            }
        }

        public Tuple<double, double> GetRandomSpeed(int min, int max)
        {
            return new Tuple<double, double>(Rnd.NextDouble(min, max), Rnd.NextDouble(min, max));
        }
    }
}