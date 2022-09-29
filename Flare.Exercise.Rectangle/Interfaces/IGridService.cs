using Flare.Exercise.Rectangle.Models;

namespace Flare.Exercise.Rectangle.Interfaces
{
    /// <summary>
    /// Responsible for the processing the data of the Grid
    /// </summary>
    public interface IGridService
    {
        /// <summary>
        /// Generate a grid with index 0
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Grid that will contain the shapes</returns>
        IGrid CreateGrid(int width, int height);

        /// <summary>
        /// Validating the shape before adding to the grid:
        /// - Positions on the grid are non-negative integer coordinates starting at 0
        /// - Rectangles must not extend beyond the edge of the grid
        /// - Rectangles must not overlap
        /// </summary>
        /// <param name="grid">Grid container</param>
        /// <param name="shape">Shape to be validated</param>
        /// <returns></returns>
        ValidationResultModel ValidateShape(IGrid grid, IShape shape);

        /// <summary>
        /// Adding the shape to the grid
        /// </summary>
        /// <param name="grid">Grid container</param>
        /// <param name="shape">Shape to be added</param>
        void AddShape(IGrid grid, IShape shape);

        /// <summary>
        /// By specifying the coordinates (X, Y) it will give the Shape information
        /// </summary>
        /// <param name="grid">Grid container</param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns>IShape containing the data of the selected shape</returns>
        IShape GetShape(IGrid grid, int locationX, int locationY);

        /// <summary>
        /// Removes the shape from the grid
        /// </summary>
        /// <param name="grid">Grid container</param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns>true when sucesfully removed or false if the shape does not exists</returns>
        bool RemoveShape(IGrid grid, int locationX, int locationY);
    }
}
