using Flare.Exercise.Rectangle.Helpers;
using Flare.Exercise.Rectangle.Interfaces;
using Flare.Exercise.Rectangle.Models;
using Flare.Exercise.Rectangle.Services;
using System;

namespace Flare.Exercise.Rectangle
{
    /// <summary>
    /// System to track the position of a collection of rectangles on a grid
    /// 
    /// ● A grid must have a width and height of no less than 5 and no greater than 25
    /// ● Positions on the grid are non-negative integer coordinates starting at 0
    /// ● Rectangles must not extend beyond the edge of the grid
    /// ● Rectangles must not overlap
    /// 
    /// Example output:
    /// Console.WriteLine("  |0 |2 |3 |4 |5 |6 |7 |8 |9 |10|");
    /// Console.WriteLine("0 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("1 |██████████████|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("2 |██████████████|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("3 |██████████████|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("4 |██████████████|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("5 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("6 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("7 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("8 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("9 |░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// Console.WriteLine("10|░░|░░|░░|░░|░░|░░|░░|░░|░░|░░|");
    /// </summary>
    internal class Program
    {
        private static IGridService _gridService;
        static void Main()
        {
            Console.WriteLine("Hello!");
            Console.WriteLine();

            _gridService = new GridService();

            // Initialy generates a from the user Grid
            IGrid grid = GetUserInputGridShape();
            PrintGrid(grid);

            bool isContinue = true;

            while (isContinue)
            {
                var selectedOption = UserMenuOptions();

                switch (selectedOption)
                {
                    case 0: // Display Grid
                        break;
                    case 1: // Create new Grid
                        grid = GetUserInputGridShape();
                        break;
                    case 2: // Add new Rectangle
                        GetUserInputRectangleShape(grid);
                        break;
                    case 3: // Search rectangle
                        SearchRectangle(grid);
                        break;
                    case 4: // Remove rectangle
                        RemoveRectangle(grid);
                        break;
                    case 5: // Exit
                        isContinue = false;
                        break;
                }

                PrintGrid(grid);
            }
        }

        /// <summary>
        /// Generates a Menu for the user to navigate
        /// 0 | Display Grid
        /// 1 | Create new grid
        /// 2 | Add new rectangle
        /// 3 | Search rectangle
        /// 4 | Remove rectangle
        /// 5 | Exit
        /// </summary>
        /// <returns>Number of the menu selected by the user</returns>
        private static int UserMenuOptions()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Menu:");
            Console.WriteLine("    0 | Display Grid");
            Console.WriteLine("    1 | Create new grid");
            Console.WriteLine("    2 | Add new rectangle");
            Console.WriteLine("    3 | Search rectangle");
            Console.WriteLine("    4 | Remove rectangle");
            Console.WriteLine("    5 | Exit");
            Console.WriteLine("----------------------------------------------");

            var selectedOption = GetUserInput("Enter a number from the options:", 0, 5);

            return selectedOption;
        }

        /// <summary>
        /// This method is responsible for generating the ASCII text on the Console App
        /// </summary>
        /// <param name="grid">Generated Grid that holds the shapes</param>
        private static void PrintGrid(IGrid grid)
        {
            Console.Clear();
            Console.CursorTop = grid.LocationY;
            Console.CursorLeft = grid.LocationX;

            // Print Grid Header
            string columnHeader = "   ";

            for (int counter = 0; counter < grid.Width; counter++)
            {
                columnHeader += $" {counter} ".Substring(0, 3);
            }

            Console.WriteLine(columnHeader);

            // Print Grid Body
            for (int heightCounter = 0; heightCounter < grid.Height; heightCounter++)
            {
                var rowNumberHeader = $"{heightCounter} ".Substring(0, 2);
                Console.Write($" {rowNumberHeader}");
                var previousColor = ConsoleColor.White;
                for (int widthCounter = 0; widthCounter < grid.Width; widthCounter++)
                {
                    var gridCell = grid.GridCells[widthCounter, heightCounter];
                    if (string.IsNullOrEmpty(gridCell.ShapeGUID))
                    {
                        Console.Write(Settings.DividerSymbol);
                    }
                    else if (previousColor != gridCell.Color && !string.IsNullOrEmpty(gridCell.ShapeGUID))
                    {
                        Console.Write(Settings.DividerSymbol);
                    }

                    previousColor = gridCell.Color;
                    Console.ForegroundColor = gridCell.Color;
                    Console.Write(grid.GridCells[widthCounter, heightCounter].DisplayText);
                    Console.ResetColor();
                }
                Console.WriteLine(Settings.DividerSymbol);
            }
        }

        /// <summary>
        /// Generates a new grid using the GridService and the specified size by the user
        /// </summary>
        /// <returns>IGrid container of the shapes</returns>
        private static IGrid GetUserInputGridShape()
        {
            var gridWidth = GetUserInput("Enter the Grid Width:", Settings.MinimumGridCell, Settings.MaximumGridCell);
            var gridHeight = GetUserInput("Enter the Grid Height:", Settings.MinimumGridCell, Settings.MaximumGridCell);

            var grid = _gridService.CreateGrid(gridWidth, gridHeight);

            return grid;
        }

        /// <summary>
        /// Generate a rectangle based from the specified size, coordinates and color of the user
        /// the rectangle will be validated before adding to the Grid Shapes
        /// </summary>
        /// <param name="grid">Grid that holds the shapes</param>
        private static void GetUserInputRectangleShape(IGrid grid)
        {
            var rectanglePositionX = GetUserInput("Enter the Rectangle X coordinates:", 0, Settings.MaximumGridCell);
            var rectanglePositionY = GetUserInput("Enter the Rectangle Y coordinates:", 0, Settings.MaximumGridCell);
            var rectangleWidth = GetUserInput("Enter the Rectangle Width:", 1, Settings.MaximumGridCell);
            var rectangleHeight = GetUserInput("Enter the Rectangle Height:", 1, Settings.MaximumGridCell);
            var rectangleSelected = GetUserInput("Enter the number of the rectangle color, 1 - Blue / 2 - Red / 3 - Green / 4 - Gray / 5 - Yellow:", 1, 5);

            var rectangelColor = ConsoleColor.White;
            switch (rectangleSelected)
            {
                case 1:
                    rectangelColor = ConsoleColor.Blue;
                    break;
                case 2:
                    rectangelColor = ConsoleColor.Red;
                    break;
                case 3:
                    rectangelColor = ConsoleColor.Green;
                    break;
                case 4:
                    rectangelColor = ConsoleColor.DarkGray;
                    break;
                case 5:
                    rectangelColor = ConsoleColor.Yellow;
                    break;
            }

            var rectangleShape = new RectangleModel(rectangleWidth, rectangleHeight, rectanglePositionX, rectanglePositionY, rectangelColor);

            var validationResult = _gridService.ValidateShape(grid, rectangleShape);

            if (validationResult.IsSuccesful)
            {
                _gridService.AddShape(grid, rectangleShape);
            }
            else
            {
                Console.WriteLine(validationResult.ErrorMessage);
                Console.WriteLine("Press any key to go back to menu...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Find a rectangle based on a given position (X, Y) and display the details
        /// </summary>
        /// <param name="grid"></param>
        private static void SearchRectangle(IGrid grid)
        {
            Console.WriteLine("Searching for the shape....");
            var locationX = GetUserInput("Enter the X coordinates:", 0, Settings.MaximumGridCell);
            var locationY = GetUserInput("Enter the Y coordinates:", 0, Settings.MaximumGridCell);
            
            var shape = _gridService.GetShape(grid, locationX, locationY);

            if (shape == null)
            {
                Console.WriteLine($"No shapes found for the coordinates (X={locationX}, Y={locationY})");
                Console.WriteLine("Press any key to go back to menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Shape is found:");
                Console.WriteLine($"GUID: {shape.GUID}");
                Console.WriteLine($"Color: {shape.Color}");
                Console.WriteLine($"Coordinats: (X={shape.LocationX}, Y={shape.LocationY})");
                Console.WriteLine($"Size: (W={shape.Width}, H={shape.Height})");
                Console.WriteLine("Press any key to go back to menu...");
                Console.ReadKey();
            }

        }

        /// <summary>
        /// Removes the shape from the Grid
        /// </summary>
        /// <param name="grid">Grid that contains the shapes</param>
        private static void RemoveRectangle(IGrid grid)
        {
            Console.WriteLine("Removing the shape....");
            var locationX = GetUserInput("Enter the X coordinates:", 0, Settings.MaximumGridCell);
            var locationY = GetUserInput("Enter the Y coordinates:", 0, Settings.MaximumGridCell);

            var isRemoved = _gridService.RemoveShape(grid, locationX, locationY);

            if (!isRemoved)
            {
                Console.WriteLine($"No shapes found for the coordinates (X={locationX}, Y={locationY})");
                Console.WriteLine("Press any key to go back to menu...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Method that gets the valid input from the user
        /// </summary>
        /// <param name="message">Help text for the user</param>
        /// <param name="minimum">Minimum value to be entered by the user</param>
        /// <param name="maximum">Maximum value to be entered by the user</param>
        /// <returns>A number between the minimum and maximum number</returns>
        private static int GetUserInput(string message, int minimum, int maximum)
        {
            Console.Write(message);
            var userInputString = Console.ReadLine();

            if (int.TryParse(userInputString, out int userInputInt)
                && userInputInt >= minimum
                    && userInputInt <= maximum)
            {
                return userInputInt;
            }
            else
            {
                Console.WriteLine($"Invalid value, {minimum} is the minimum and {maximum} is the maximum value allowed.");
                return GetUserInput(message, minimum, maximum);
            }
        }
    }
}