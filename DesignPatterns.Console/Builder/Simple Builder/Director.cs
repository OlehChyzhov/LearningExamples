namespace DesignPatterns.Builder.Solution
{
    /// <summary>
    /// Director defines the order in which we should call the construction steps so that we can reuse specific 
    /// configurations of the product. It is especially useful when the construction steps are complex
    /// or frequently repeated.
    /// </summary>

    public class Director
    {
        // Those methods configure builder. After that you just need to call "Build" method from builder.
        public void BuildBuggatti(CarBuilder builder)
        {
            builder.Brand("Bugatti")
                .Color("Blue")
                .Model("Divo")
                .Id(2215);
        }

        public void BuildLambo(CarBuilder builder)
        {
            builder.Brand("Lamborghini")
                .Color("Yellow")
                .Model("Aventador")
                .Id(5313);
        }
    }


    #region Example
    public static class Director_Example
    {
        public static void Run()
        {
            Director director = new Director();
            CarBuilder carBuilder = new CarBuilder();

            // Configured car from templates
            director.BuildBuggatti(carBuilder);

            // Changed only color
            carBuilder.Color("Dark Orange");

            Console.WriteLine(carBuilder.Build());
        }
    }
    #endregion
}
