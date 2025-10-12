namespace DesignPatterns.Factories.Abstract_Factory
{
    /*
     * ABSTRACT FACTORY
     * 
     * The Abstract Factory pattern provides an interface for creating families of related or dependent objects 
     * without specifying their concrete classes. 
     * 
     * It's particularly useful when:
     * - Objects in a group are meant to work together (e.g., MSI GPU and MSI Monitor).
     * - The system needs to be independent of how these objects are created or composed.
     * - You want to avoid conditional logic (e.g., if-else or switch) when creating families of objects.
     */

    // Let's imagine a company that manufactures computer parts.

    public interface Component
    {
        public void Assemble(); // All components must implement this method.
    }

    // You have two types of GPUs.
    public class ProblematicMsiGPU : Component
    {
        public void Assemble() {}
    }

    public class ProblematicAsusGPU : Component
    {
        public void Assemble() {}
    }

    // And two types of monitors.
    public class ProblematicMsiMonitor : Component
    {
        public void Assemble() {}
    }

    public class ProblematicAsusMonitor : Component
    {
        public void Assemble() {}
    }

    // To group the components by brand, you create manufacturer-specific classes.
    public abstract class Company
    {
        // But you're forced to use a string parameter to specify the type of component to create,
        // which is error-prone and hard to maintain.
        public abstract Component CreateComponent(string type);
    }

    public class MsiManufacturer : Company
    {
        public override Component CreateComponent(string type)
        {
            // You have to rely on string comparison, which is fragile and violates the Open/Closed Principle.
            // Every time a new component type is added, this method must change.

            if ("GPU".Equals(type))
                return new ProblematicMsiGPU();
            else
                return new ProblematicMsiMonitor();
        }
    }

    public class AsusManufacturer : Company
    {
        public override Component CreateComponent(string type)
        {
            // The same problem

            if ("GPU".Equals(type))
                return new ProblematicAsusGPU();
            else
                return new ProblematicAsusMonitor();
        }
    }
}