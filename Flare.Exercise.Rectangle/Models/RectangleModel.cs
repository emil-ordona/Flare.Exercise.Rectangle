using Flare.Exercise.Rectangle.Interfaces;
using System;

namespace Flare.Exercise.Rectangle.Models
{
    public class RectangleModel : IShape
    {
        private readonly string _guid;
        private readonly int _width;
        private readonly int _height;
        private readonly int _locationX;
        private readonly int _locationY;
        private readonly ConsoleColor _color;

        public RectangleModel(int width, int height, int locationX, int locationY, ConsoleColor color)
        {
            _guid = Guid.NewGuid().ToString();
            _width = width;
            _height = height;
            _locationX = locationX;
            _locationY = locationY;
            _color = color;
        }

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
