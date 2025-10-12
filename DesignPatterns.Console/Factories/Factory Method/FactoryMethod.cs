namespace DesignPatterns.Factories.Factory_Method
{
    // Factory Method pattern encapsulates object creation inside static methods,
    // providing named constructors that clarify intent and support different creation logic.

    public class Point
    {
        private double x, y;

        // Private constructor prevents direct instantiation from outside,
        // forcing the use of factory methods for creating instances.
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Factory Method for creating a point from Cartesian coordinates.
        // Makes the creation intention explicit and clear.
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // Factory Method for creating a point from Polar coordinates.
        // Converts polar to Cartesian internally, hiding complexity.
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        // Factory method can also handle async initialization,
        // which constructors cannot do because constructors can't be async.
        public async static Task<Point> NewCartesianPointAsync(double x, double y)
        {
            await Task.Delay(1000); // Simulate async work (e.g., I/O, computation)
            return new Point(x, y);
        }

    }

    #region Example
    public static class FactoryMethod_Example
    {
        public static void Run()
        {
            // Using factory methods clarifies how points are created
            Point point = Point.NewCartesianPoint(3, 6);
            Point polarPoint = Point.NewPolarPoint(1.0, Math.PI / 2);
        }
    }
    #endregion
}
