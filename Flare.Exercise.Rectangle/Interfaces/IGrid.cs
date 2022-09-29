using System.Collections.Generic;

namespace Flare.Exercise.Rectangle.Interfaces
{
    /// <summary>
    /// Container for the shapes
    /// </summary>
    public interface IGrid : IShape
    {
        /// <summary>
        /// A collection of shapes that is displayed inside the grid
        /// </summary>
        List<IShape> Shapes { get; } 

        /// <summary>
        /// The memory path of the shapes
        /// </summary>
        IGridCell[,] GridCells { get; }
    }
}
