using System;

namespace Flare.Exercise.Rectangle.Interfaces
{
    /// <summary>
    /// A shape that can be drawn inside the grid/console app
    /// </summary>
    public interface IShape
    {
        string GUID { get; }

        int Width { get; }
        
        int Height { get; }

        int LocationX { get; }

        int LocationY { get; }

        ConsoleColor Color { get; }
    }
}
