namespace DesignPatterns.Builder.Solution.Stepwise_Builder
{
    // This type of builder uses multiple interfaces to enforce a step-by-step object construction process.
    // Each step returns a different interface, guiding the client through a valid construction flow.
    // This technique prevents skipping mandatory steps and ensures compile-time safety.

    // First step: specify the car type
    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    // Second step: specify the wheel size
    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    // Final step: build the car
    public interface IBuildCar
    {
        Car Build();
    }

    public static class StepwiseCarBuilder
    {
        #region Inner Implementation Of Interfaces
        // Internal implementation of all builder steps.
        // This class is private and only accessible via the public Create() method.
        private class Implementation : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private readonly Car car = new Car();

            // Step 1: set the car type and proceed to the next step.
            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            // Step 2: set the wheel size with type-specific validation logic.
            public IBuildCar WithWheels(int size)
            {
                // Validate that the selected wheel size is appropriate for the chosen car type.
                switch (car.Type)
                {
                    case CarType.Sedan when size < 17 || size > 20:
                    case CarType.Crossover when size < 15 || size > 17:
                        throw new ArgumentException($"Invalid wheel size {size} for car type {car.Type}");
                }

                car.WheelSize = size;
                return this;
            }

            // Final step: return the fully constructed car.
            public Car Build()
            {
                return car;
            }
        }
        #endregion

        /// <summary>
        /// Entry point for building a car using the stepwise builder.
        /// The returned interface enforces that the first step must be specifying the car type.
        /// </summary>
        public static ISpecifyCarType Create()
        {
            return new Implementation();
        }
    }

    #region Example
    public static class StepwiseBuilder_Example
    {
        public static void Run()
        {
            // This example demonstrates a safe and enforced build process for a Sedan car with valid wheel size.
            Car car = StepwiseCarBuilder.Create()
                .OfType(CarType.Sedan)
                .WithWheels(18)
                .Build();

            Console.WriteLine(car);
        }
    }
    #endregion
}