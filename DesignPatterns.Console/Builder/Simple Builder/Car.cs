namespace DesignPatterns.Builder.Solution
{
    // The target object to be constructed using the Builder pattern.
    public class Car
    {
        private int id;
        private string brand;
        private string model;
        private string color;
        // Additional fields...

        // Constructor is internal to restrict direct instantiation outside the assembly.
        internal Car(int id, string brand, string model, string color)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

        public override string ToString()
        {
            return $"Id: {id}, Brand: {brand}, Model: {model}, Color: {color}";
        }
    }
}
