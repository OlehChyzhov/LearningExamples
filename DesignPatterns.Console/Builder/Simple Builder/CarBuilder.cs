namespace DesignPatterns.Builder.Solution
{
    // Builder class responsible for constructing Car instances.
    public class CarBuilder
    {
        private int id;
        private string brand;
        private string model;
        private string color;
        // Additional fields...


        // Each method returns "this" so that those methods can be chained together. This called "Fluent Builder"
        public CarBuilder Id(int id) { this.id = id; return this; }
        public CarBuilder Brand(string brand) { this.brand = brand; return this; }
        public CarBuilder Model(string model) { this.model = model; return this; }
        public CarBuilder Color(string color) { this.color = color; return this; }
        // Additional builder methods...


        /// <summary> Builds and returns the Car instance. </summary>
        public Car Build()
        {
            return new Car(id, brand, model, color);
        }
    }


    #region Example
    public static class Builder_Example
    {
        public static void Run()
        {
            CarBuilder carBuilder = new CarBuilder();

            // Now creation of new car looks much better:
            carBuilder.Id(1).Brand("Audi").Model("A6").Color("Black");
            Car builtCar = carBuilder.Build();
            Console.WriteLine(builtCar);
        }
    }
    #endregion
}