using System;

namespace Flare.Exercise.Rectangle.Interfaces
{
    /// <summary>
    /// Represent each character/text displayed inside the Grid
    /// </summary>
    public interface IGridCell
    {
        string ShapeGUID { get; }

        ConsoleColor Color { get; }

        string DisplayText { get; }
    }
}
