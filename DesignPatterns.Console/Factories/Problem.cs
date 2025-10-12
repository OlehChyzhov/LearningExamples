namespace DesignPatterns.Factories
{
    // Factory patterns encapsulate object creation,
    // letting you create objects without exposing complex construction details.
    //
    // Use factories when constructors become confusing, overloaded, or insufficient,
    // or when you want to decouple clients from concrete classes.
    //
    // Why not just constructors?
    // - Constructors have limited flexibility: they can't be overloaded by parameter names,
    //   only by types and count, which limits meaningful variations.
    // - Constructors can't express complex creation logic clearly.
    // - Using constructors alone leads to unclear, error-prone code (e.g. vague parameter names).
    // - Factories can return different subclasses or cached instances; constructors cannot.
    // - Factories help decouple client code from concrete implementations,
    //   making your codebase more flexible and maintainable.

    public enum CoordinateSystem { Cartesian, Polar }

    public class Point
    {
        private double x, y;

        // Constructor for Cartesian coordinates: clear and simple.
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Problem: We cannot create another constructor with the same signature (double, double)
        // for Polar coordinates because constructor overloading in C# depends on parameter types,
        // not on parameter names.

        // So, we use this workaround: a neutral constructor with parameters (a, b)
        // and a CoordinateSystem enum to indicate how to interpret them.
        // This has several downsides:
        // 1) Parameter names are non-descriptive ('a', 'b'), losing semantic meaning.
        // 2) Callers must remember to specify the coordinate system.
        // 3) The constructor is less readable and more error-prone.
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a; // 'a' is X here
                    y = b; // 'b' is Y here
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b); // 'a' is rho (radius), 'b' is theta (angle)
                    y = a * Math.Sin(b);
                    break;
            }
        }

        public override string ToString() => $"Point: (x={x}, y={y})";
    }

    public static class NoFactoryProblem_Exam
    {
        public static void Run()
        {
            // Cartesian creation is clear:
            var p1 = new Point(3, 4);

            // Polar creation requires specifying the enum and uses unclear parameters:
            var p2 = new Point(5, Math.PI / 2, CoordinateSystem.Polar);

            // Here, without reading documentation or parameter names,
            // it’s hard to know what '5' and 'Math.PI / 2' represent.
            // Misuse or confusion is likely if you forget the enum or swap values.

            System.Console.WriteLine(p1); // Output: Point: (x=3, y=4)
            System.Console.WriteLine(p2); // Output: Point: (x=0, y=5)
        }
    }
}
