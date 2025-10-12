namespace DesignPatterns.Builder
{
    // The Builder pattern separates the construction of a complex object
    // from its representation, allowing the same construction process to create different representations.

    // It is useful when an object requires multiple steps to be created
    // or when a constructor has too many parameters (especially optional ones).

    public class ProblematicCar
    {
        // The Car class contains many configuration fields.
        private string brand;
        private string model;
        private string color;
        private int numberOfDoors;
        private string? screenType;
        private double weight;
        private double? height;

        // The constructor requires many parameters to initialize the object.
        // Some parameters are optional, which leads to multiple overloaded constructors.
        // This makes object creation error-prone, hard to maintain, and difficult to read.
        public ProblematicCar(string brand, string model, string color, int numberOfDoors, string? screenType, double weight, double? height)
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
            this.numberOfDoors = numberOfDoors;
            this.screenType = screenType;
            this.weight = weight;
            this.height = height;
        }
    }
}
