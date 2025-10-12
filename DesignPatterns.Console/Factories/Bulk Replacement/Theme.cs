namespace DesignPatterns.Factories.Bulk_Replacement
{
    // Interface defining a UI theme with text and background colors
    public interface ITheme
    {
        string TextColor { get; }
        string BackgroundColor { get; }
    }

    // Light theme implementation
    class LightTheme : ITheme
    {
        public string TextColor => "Black";
        public string BackgroundColor => "White";
    }

    // Dark theme implementation
    class DarkTheme : ITheme
    {
        public string TextColor => "White";
        public string BackgroundColor => "Black";
    }

}
