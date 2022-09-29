using Flare.Exercise.Rectangle.Helpers;
using Flare.Exercise.Rectangle.Interfaces;
using System;
using System.Collections.Generic;

namespace Flare.Exercise.Rectangle.Models
{
    public class GridModel : IGrid
    {
        private readonly string _guid;
        private readonly int _width;
        private readonly int _height;
        private readonly int _locationX;
        private readonly int _locationY;
        private readonly ConsoleColor _color;
        private readonly List<IShape> _shape;
        private readonly IGridCell[,] _gridCells;

        public GridModel(int width, int height, int locationX, int locationY, ConsoleColor color)
        {
            _guid = Guid.NewGuid().ToString();
            _width = width;
            _height = height;
            _locationX = locationX;
            _locationY = locationY;
            _color = color;
            _shape = new List<IShape>();
            _gridCells = new GridCellModel[width, height];

        }

        public void Initialize()
        {
            for (int heightCounter = 0; heightCounter < Height; heightCounter++)
            {
                for (int widthCounter = 0; widthCounter < Width; widthCounter++)
                {
                    _gridCells[widthCounter, heightCounter] = new GridCellModel("", ConsoleColor.White, $"{Settings.EmptyCellSymbol}{Settings.EmptyCellSymbol}");
                }
            }
        }
        public List<IShape> Shapes { get { return _shape; } }

        public IGridCell[,] GridCells { get { return _gridCells; } }

        public string GUID
        {
            get { return _guid; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height 
        {
            get { return _height; }
        }

        public int LocationX
        {
            get { return _locationX; }
        }

        public int LocationY
        {
            get { return _locationY; }
        }

        public ConsoleColor Color
        {
            get { return _color; }
        }

    }
}
