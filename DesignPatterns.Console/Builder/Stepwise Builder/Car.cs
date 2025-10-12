namespace DesignPatterns.Builder.Solution.Stepwise_Builder
{
    public enum CarType { Sedan, Crossover }
    public class Car
    {
        public CarType Type { get; set; }
        public int WheelSize { get; set; }

        public override string ToString()
        {
            return $"Type: {Type}, WheelSize: {WheelSize}";
        }
    }
}
