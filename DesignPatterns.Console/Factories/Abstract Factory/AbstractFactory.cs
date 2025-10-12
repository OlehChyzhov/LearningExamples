namespace DesignPatterns.Factories.Abstract_Factory
{
    /*
     * Steps to implement Abstract Factory:
     *
     * 1. Define separate interfaces for each type of product (e.g., Monitor, GPU).
     * 2. Create concrete implementations for each brand/type.
     * 3. Define an abstract factory that declares creation methods for each product.
     * 4. Create concrete factories that implement the creation methods to produce specific variants.
     */

    // 1. Explicitly declare interfaces for each distinct product
    public interface Monitor
    {
        void Assemble();
    }

    public interface GPU
    {
        void Assemble();
    }

    // 2. Concrete implementations of products (for MSI)
    public class MsiMonitor : Monitor
    {
        public void Assemble()
        {
            // MSI-specific monitor assembly logic
        }
    }

    public class MsiGPU : GPU
    {
        public void Assemble()
        {
            // MSI-specific GPU assembly logic
        }
    }

    // Concrete implementations of products (for ASUS)
    public class AsusMonitor : Monitor
    {
        public void Assemble()
        {
            // ASUS-specific monitor assembly logic
        }
    }

    public class AsusGPU : GPU
    {
        public void Assemble()
        {
            // ASUS-specific GPU assembly logic
        }
    }

    // 3. Abstract Factory: defines interface for creating each component
    public abstract class AbstractFactory
    {
        public abstract GPU CreateGPU();
        public abstract Monitor CreateMonitor();
    }

    // 4. Concrete Factory for MSI
    public class MsiConcreteFactory : AbstractFactory
    {
        public override GPU CreateGPU()
        {
            return new MsiGPU();
        }

        public override Monitor CreateMonitor()
        {
            return new MsiMonitor();
        }
    }

    // Concrete Factory for ASUS
    public class AsusConcreteFactory : AbstractFactory
    {
        public override GPU CreateGPU()
        {
            return new AsusGPU();
        }

        public override Monitor CreateMonitor()
        {
            return new AsusMonitor();
        }
    }

    /*
     * The Abstract Factory pattern is useful when you need to create families of related objects
     * without depending on their concrete classes. It ensures that components from the same family
     * (e.g., MSI GPU + MSI Monitor) are used together, which keeps product variants consistent.
     * 
     * ✔️ Solves:
     * - Avoids conditionals (no need for if/switch to choose brand/type).
     * - Follows Open/Closed Principle (If new parts are added).
     * - Promotes consistency across related products.
     */

    #region Example
    public class AbstractFactory_Example
    {
        public static void Run()
        {
            // Let's say the client wants MSI components
            AbstractFactory msiFactory = new MsiConcreteFactory();
            GPU msiGpu = msiFactory.CreateGPU();
            Monitor msiMonitor = msiFactory.CreateMonitor();

            msiGpu.Assemble();
            msiMonitor.Assemble();

            // Now the client wants ASUS components
            AbstractFactory asusFactory = new AsusConcreteFactory();
            GPU asusGpu = asusFactory.CreateGPU();
            Monitor asusMonitor = asusFactory.CreateMonitor();

            asusGpu.Assemble();
            asusMonitor.Assemble();
        }
    }
    #endregion
}
