using Flare.Exercise.Rectangle.Helpers;
using Flare.Exercise.Rectangle.Interfaces;
using Flare.Exercise.Rectangle.Models;
using System;
using System.Linq;

namespace Flare.Exercise.Rectangle.Services
{
    public class GridService : IGridService
    {
        public IGrid CreateGrid(int width, int height)
        {
            if (width < 5 || width > 25)
            {
                Console.WriteLine("Grid width minimum size is 5 until 25.");
                return null;
            }

            if (height < 5 || height > 25)
            {
                Console.WriteLine("Grid height minimum size is 5 until 25.");
                return null;
            }

            var grid = new GridModel(width + 1, height + 1, 0, 0, ConsoleColor.White);
            grid.Initialize();

            return grid;
        }

        public ValidationResultModel ValidateShape(IGrid grid, IShape shape)
        {
            if (shape.LocationX + shape.Width > grid.Width)
            {
                return new ValidationResultModel(false, "The shape's width is already out of the grids coverage.");
            }

            if (shape.LocationY + shape.Height > grid.Height)
            {
                return new ValidationResultModel(false, "The shape's height is already out of the grids coverage.");
            }

            for (int heightCounter = shape.LocationY; heightCounter < shape.LocationY + shape.Height; heightCounter++)
            {
                for (int widthCounter = shape.LocationX; widthCounter < shape.LocationX + shape.Width; widthCounter++)
                {
                    var gridCell = grid.GridCells[widthCounter, heightCounter];
                    if (!string.IsNullOrEmpty(gridCell.ShapeGUID))
                    {
                        var gridShape = grid.Shapes.Find(c => c.GUID == gridCell.ShapeGUID);
                        return new ValidationResultModel(false, $"Unable to add the shape, overlapping with another shape. GUID: {gridShape.GUID}, Color: {gridShape.Color}, Coordinates: (X={gridShape.LocationX}, Y={gridShape.LocationY}), Size=(W={gridShape.Width}, H={gridShape.Height})");
                    }
                }
            }

            return new ValidationResultModel(true, string.Empty);
        }

        public void AddShape(IGrid grid, IShape shape)
        {
            for (int heightCounter = shape.LocationY; heightCounter < shape.LocationY + shape.Height; heightCounter++)
            {
                for (int widthCounter = shape.LocationX; widthCounter < shape.LocationX + shape.Width; widthCounter++)
                {
                    grid.GridCells[widthCounter, heightCounter] = new GridCellModel(shape.GUID, shape.Color, $"{(widthCounter == shape.LocationX ? "" : Settings.FilledCellSymbol)}{Settings.FilledCellSymbol}{Settings.FilledCellSymbol}");
                }
            }

            grid.Shapes.Add(shape);
        }

        public IShape GetShape(IGrid grid, int locationX, int locationY)
        {
            if (locationX > grid.Width)
            {
                return null;
            }

            if (locationY > grid.Height)
            {
                return null;
            }

            var gridCell = grid.GridCells[locationX, locationY];
            var gridShape = grid.Shapes.FirstOrDefault(c => c.GUID == gridCell.ShapeGUID);

            return gridShape;
        }

        public bool RemoveShape(IGrid grid, int locationX, int locationY)
        {
            if (locationX > grid.Width)
            {
                return false;
            }

            if (locationY > grid.Height)
            {
                return false;
            }

            var gridCell = grid.GridCells[locationX, locationY];
            var gridShape = grid.Shapes.FirstOrDefault(c => c.GUID == gridCell.ShapeGUID);

            if (gridShape == null)
            {
                return false;
            }

            for (int heightCounter = gridShape.LocationY; heightCounter < gridShape.LocationY + gridShape.Height; heightCounter++)
            {
                for (int widthCounter = gridShape.LocationX; widthCounter < gridShape.LocationX + gridShape.Width; widthCounter++)
                {
                    grid.GridCells[widthCounter, heightCounter] = new GridCellModel("", ConsoleColor.White, $"{Settings.EmptyCellSymbol}{Settings.EmptyCellSymbol}");
                }
            }

            grid.Shapes.Remove(gridShape);

            return true;
        }
    }
}
