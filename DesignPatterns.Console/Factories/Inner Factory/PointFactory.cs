/*
 * An Inner Factory is a static class defined inside the product class itself (e.g., Point.Factory)
 * and is used to encapsulate and organize factory methods directly related to that product.
 *
 * The simple factory requires the constructor to be public or internal, which may expose object
 * creation. An inner factory solves this by being nested within the class, allowing it to access a private constructor
 */

namespace DesignPatterns.Factories.Inner_Factory
{
    /// <summary>
    /// Represents a point in 2D space.
    /// The constructor is private, so instances can only be created through factory methods.
    /// </summary>
    public class Point
    {
        private double x, y;

        // Constructor is private to restrict object creation.
        // Enforces the use of factory methods for object instantiation.
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Inner static factory class responsible for creating instances of Point.
        /// Helps separate the construction logic from the Point class itself.
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// Creates a Point using Cartesian coordinates (x, y).
            /// </summary>
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            /// <summary>
            /// Creates a Point using Polar coordinates (rho, theta).
            /// </summary>
            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    #region Example

    /// <summary>
    /// Demonstrates the usage of the inner factory pattern.
    /// Client code uses clearly named factory methods to specify intent (Cartesian or Polar).
    /// </summary>
    public static class Factory_Example
    {
        public static void Run()
        {
            // Creates a point using Cartesian coordinates.
            Point pointCartesian = Point.Factory.NewCartesianPoint(2, 5);

            // Creates a point using Polar coordinates.
            Point pointPolar = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
        }
    }

    #endregion
}
