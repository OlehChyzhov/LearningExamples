namespace DesignPatterns.Factories.Factory
{
    // Factory pattern separates object creation from the class itself.
    // This is useful when:
    // - You want to keep constructors simple or inaccessible,
    // - You want multiple ways to create an object without cluttering the class,
    // - You want to centralize creation logic outside the product class.

    public class Point
    {
        private double x, y;

        // Public constructor since factory is a separate class.
        // The product class doesn’t control instantiation here.
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class PointFactory
    {
        // Creates a Point using Cartesian coordinates.
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // Creates a Point from Polar coordinates.
        // Converts polar to Cartesian internally, hiding conversion details.
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    #region Example
    public static class Factory_Example
    {
        public static void Run()
        {
            // Client code clearly states intent by calling factory methods.
            Point pointCartesian = PointFactory.NewCartesianPoint(2, 5);
            Point pointPolar = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
        }
    }
    #endregion
}