using Flare.Exercise.Rectangle.Interfaces;
using System;

namespace Flare.Exercise.Rectangle.Models
{
    public class GridCellModel : IGridCell
    {
        private readonly string _shapeGUId;
        private readonly ConsoleColor _color;
        private readonly string _displayText;

        public GridCellModel(string shapeGUId, ConsoleColor color, string displayText)
        {
            _shapeGUId = shapeGUId;
            _color = color;
            _displayText = displayText;
        }

        public string ShapeGUID
        {
            get { return _shapeGUId; }
        }

        public ConsoleColor Color
        {
            get { return _color; }
        }

        public string DisplayText
        {
            get { return _displayText; }
        }
    }
}
