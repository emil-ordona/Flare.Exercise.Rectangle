namespace Flare.Exercise.Rectangle.Helpers
{
    public static class Settings
    {
        /// <summary>
        /// ASCII symbols used by PrintGrid() to display on the console app
        /// </summary>
        public const string EmptyCellSymbol = "░";
        public const string FilledCellSymbol = "█";
        public const string DividerSymbol = "│";

        /// <summary>
        /// The limit constraints of grid size
        /// </summary>
        public const int MinimumGridCell = 5;
        public const int MaximumGridCell = 25;

    }
}
