using Flare.Exercise.Rectangle.Interfaces;
using Flare.Exercise.Rectangle.Models;
using Flare.Exercise.Rectangle.Services;

namespace Flare.Exercise.Rectangle.Test
{
    [TestClass]
    public class GridServiceTest
    {

        [TestMethod]
        [DataRow(5,5)]
        [DataRow(6,5)]
        [DataRow(7,5)]
        [DataRow(8,5)]
        [DataRow(9,5)]
        [DataRow(10,5)]
        [DataRow(14,5)]
        [DataRow(15,5)]
        [DataRow(21,5)]
        [DataRow(24,5)]
        [DataRow(25,5)]
        public void CreateGridMinMaxWidth(int width, int height)
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(width, height);

            Assert.IsNotNull(grid);
        }

        [TestMethod]
        [DataRow(5,5)]
        [DataRow(5,6)]
        [DataRow(5,7)]
        [DataRow(5,8)]
        [DataRow(5,9)]
        [DataRow(5,21)]
        [DataRow(5,22)]
        [DataRow(5,23)]
        [DataRow(5,24)]
        [DataRow(5,25)]
        public void CreateGridMinMaxHeight(int width, int height)
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(width, height);

            Assert.IsNotNull(grid);
        }

        [TestMethod]
        [DataRow(0,-2)]
        [DataRow(-1,5)]
        [DataRow(1,5)]
        [DataRow(4,6)]
        [DataRow(55,77)]
        [DataRow(26,8)]
        public void CreateGridOutOfRangeWidth(int width, int height)
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(width, height);

            Assert.IsNull(grid);
        }

        [TestMethod]
        [DataRow(5, 0)]
        [DataRow(52, 4)]
        [DataRow(4, 55)]
        [DataRow(5, 26)]
        public void CreateGridOutOfRangeHeight(int width, int height)
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(width, height);

            Assert.IsNull(grid);
        }

        [TestMethod]
        public void ValidateShapeTest_1cell()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(1, 1, 5, 5, ConsoleColor.White);

            var validationResult = gridService.ValidateShape(grid, shape);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "");
        }

        [TestMethod]
        public void ValidateShapeTest_5cell()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(0, 0, 5, 5, ConsoleColor.White);

            var validationResult = gridService.ValidateShape(grid, shape);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "");
        }

        [TestMethod]
        public void ValidateShapeTest_OutOfBorder()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(0, 1, 5, 6, ConsoleColor.White);

            var validationResult = gridService.ValidateShape(grid, shape);

            Assert.IsNotNull(validationResult);
            Assert.IsFalse(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "The shape's height is already out of the grids coverage.");
        }

        [TestMethod]
        public void ValidateShapeTest_Overlap()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(5, 6, 0, 0, ConsoleColor.Red);

            gridService.AddShape(grid, shape);

            var shape2 = new RectangleModel(3, 3, 0, 0, ConsoleColor.Green);

            var validationResult = gridService.ValidateShape(grid, shape2);

            Assert.IsNotNull(validationResult);
            Assert.IsFalse(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, $"Unable to add the shape, overlapping with another shape. GUID: {shape.GUID}, Color: Red, Coordinates: (X=0, Y=0), Size=(W=5, H=6)");
        }

        [TestMethod]
        public void ValidateShapeTest_MultipleShapes()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(5, 6, 0, 0, ConsoleColor.Blue);

            var validationResult = gridService.ValidateShape(grid, shape);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "");

            gridService.AddShape(grid, shape);

            var shape2 = new RectangleModel(1, 2, 5, 0, ConsoleColor.Red);

            validationResult = gridService.ValidateShape(grid, shape2);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "");
        
            gridService.AddShape(grid, shape2);


            var shape3 = new RectangleModel(1, 4, 5, 2, ConsoleColor.Green);

            validationResult = gridService.ValidateShape(grid, shape3);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.IsSuccesful);
            Assert.AreEqual(validationResult.ErrorMessage, "");

            gridService.AddShape(grid, shape3);

        }


        [TestMethod]
        public void GetShape_ValidTest()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(5, 6, 0, 0, ConsoleColor.Blue);
            gridService.AddShape(grid, shape);

            var shape2 = new RectangleModel(1, 2, 5, 0, ConsoleColor.Red);
            gridService.AddShape(grid, shape2);

            var shape3 = new RectangleModel(1, 4, 5, 2, ConsoleColor.Green);
            gridService.AddShape(grid, shape3);

            var getShape1 = gridService.GetShape(grid, 4, 2);
            Assert.AreEqual(shape, getShape1);

            var getShape2 = gridService.GetShape(grid, 5, 0);
            Assert.AreEqual(shape2, getShape2);

            var getShape3 = gridService.GetShape(grid, 5, 5);
            Assert.AreEqual(shape3, getShape3);
        }

        [TestMethod]
        public void GetShape_MissingShapeAndOutOfBounds()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);

            var getShape1 = gridService.GetShape(grid, 4, 2);
            Assert.IsNull(getShape1);

            var getShape2 = gridService.GetShape(grid, 22, 33);
            Assert.IsNull(getShape2);

            var getShape3 = gridService.GetShape(grid, -5, 500);
            Assert.IsNull(getShape3);
        }

        [TestMethod]
        public void RemoveShape_ValidTest()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);
            var shape = new RectangleModel(5, 6, 0, 0, ConsoleColor.Blue);
            gridService.AddShape(grid, shape);

            var shape2 = new RectangleModel(1, 2, 5, 0, ConsoleColor.Red);
            gridService.AddShape(grid, shape2);

            var shape3 = new RectangleModel(1, 4, 5, 2, ConsoleColor.Green);
            gridService.AddShape(grid, shape3);

            var removeShape1 = gridService.RemoveShape(grid, 4, 2);
            Assert.IsTrue(removeShape1);

            var removeShape1A = gridService.RemoveShape(grid, 4, 2);
            Assert.IsFalse(removeShape1A);


            var removeShape2 = gridService.RemoveShape(grid, 5, 0);
            Assert.IsTrue(removeShape2);
            var removeShape2B = gridService.RemoveShape(grid, 5, 0);
            Assert.IsFalse(removeShape2B);


            var removeShape3 = gridService.RemoveShape(grid, 5, 5);
            Assert.IsTrue(removeShape3);
            var removeShape3B = gridService.RemoveShape(grid, 5, 5);
            Assert.IsFalse(removeShape3B);
        }

        [TestMethod]
        public void Remove_MissingShapeAndOutOfBounds()
        {
            GridService gridService = new GridService();
            var grid = gridService.CreateGrid(5, 5);

            var removeShape1 = gridService.RemoveShape(grid, 4, 2);
            Assert.IsFalse(removeShape1);

            var removeShape2 = gridService.RemoveShape(grid, 22, 33);
            Assert.IsFalse(removeShape2);

            var removeShape3 = gridService.RemoveShape(grid, -5, 500);
            Assert.IsFalse(removeShape3);
        }

    }
}