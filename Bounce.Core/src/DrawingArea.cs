using System.Collections.ObjectModel;

namespace Bounce.Core
{
    /// <summary>
    /// Abstract class you have to implement.
    /// </summary>
    public abstract class DrawingArea
    {
        /// <summary>
        /// Collection with the game objects.
        /// </summary>
        public ObservableCollection<object> Objects;

        /// <summary>
        /// Height of the drawing area.
        /// </summary>
        public abstract double Height { get; }

        /// <summary>
        /// Width of the drawing area.
        /// </summary>
        public abstract double Width { get; }

        /// <summary>
        /// Method that is called when the position of ball changes.
        /// </summary>
        /// <param name="ball">Ball with updated position.</param>
        public abstract void UpdatePosition(Ball ball);
    }
}