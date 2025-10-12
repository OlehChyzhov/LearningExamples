namespace DesignPatterns.SOLID
{
    // The Open/Closed Principle (OCP) states that software entities (classes, modules, etc.)
    // should be open for extension but closed for modification.
    // This means you should be able to add new functionality without changing existing code.

    #region Default Objects
    public enum Color { Red, Green, Blue }

    public enum Size { Small, Medium, Large, Huge }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }
    #endregion

    #region The Problem
    /// <summary>
    /// Let's imagine that you're a developer and your boss tells you that you have to filter Products by either Size or Color.
    /// You decided to create a class ProductFilter with two methods.
    /// </summary>
    public class ProductFilter
    {
        /// <summary>
        /// You created a method for filtering by size.
        /// </summary>
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (Product product in products)
            {
                if (product.Size == size)
                {
                    yield return product;
                }
            }
        }

        /// <summary>
        /// And another method for filtering by color.
        /// </summary>
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (Product product in products)
            {
                if (product.Color == color)
                {
                    yield return product;
                }
            }
        }

        // Seems like everything is good, BUT ...

        /// <summary>
        /// Unfortunately, your boss comes back and says that now he wants to filter by both Size and Color at the same time.
        /// You decided to modify this class and add a new method, but each new filtering requirement
        /// forces you to modify this class.
        /// </summary>
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (Product product in products)
            {
                if (product.Color == color && product.Size == size)
                {
                    yield return product;
                }
            }
        }
    }

    /*
     * This violates the Open/Closed Principle (OCP)."
     
     * That means you shouldn't go back to an already created class and modify its existing code just to add new functionality.
     * Instead, you should extend its behavior using new code — for example, by implementing an interface or subclass.
     
     * In real-world projects, modifying existing code can introduce bugs,
     * especially if it is already in use or deployed.
     
     * A better approach is to use interfaces to
     * make the filtering logic extensible and decoupled. */

    #endregion

    #region Solution
    /*
     * This can be done the better way:
     * Every time the requirements change, you shouldn't be forced to go back and modify the ProductFilter class again and again.
     * So you come up with a better, more flexible design. You decide to use the Specification pattern.
     */

    // This interface represents a single condition (rule) that can be checked against an object.
    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }

    // A concrete rule: checks whether a product is of a specific color.
    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product item)
        {
            return item.Color == _color;
        }
    }

    // A second concrete rule: checks whether a product is of a specific size.
    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product item)
        {
            return item.Size == _size;
        }
    }

    // This rule combines two other rules: it checks whether BOTH are satisfied.
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T item)
        {
            return _first.IsSatisfied(item) && _second.IsSatisfied(item);
        }
    }

    // Now, you need a filter that can work with these flexible rules.
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    // The improved filter doesn't care what the rule is — it just applies it.
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (Product product in items)
            {
                if (specification.IsSatisfied(product))
                {
                    yield return product;
                }
            }
        }
    }

     // Next time your boss comes back with another weird request, you won't need to touch BetterFilter at all.
     // You just create a new specification class that implements ISpecification<Product>, and you're done
    #endregion

    #region OCP_Example
    public static class OCP_Example
    {
        /// <summary>
        /// Instead of modifying filtering logic for each new rule, we use small composable specifications (rules),
        /// which can be combined and reused. This keeps our filtering logic open for extension, but closed for modification.
        /// </summary>
        public static void Run()
        {
            // Create some sample products
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            // Create the filter that works with any ISpecification<Product>
            var filter = new BetterFilter();

            // FILTER: Products that are green
            Console.WriteLine("Green Products:");
            var colorSpecification = new ColorSpecification(Color.Green);
            foreach (Product product in filter.Filter(products, colorSpecification))
            {
                Console.WriteLine($" - {product.Name} is green");
            }

            // FILTER: Products that are large
            Console.WriteLine("\nLarge Products:");
            var sizeSpecification = new SizeSpecification(Size.Large);
            foreach (Product product in filter.Filter(products, sizeSpecification))
            {
                Console.WriteLine($" - {product.Name} is large");
            }

            // FILTER: Products that are both large AND green
            Console.WriteLine("\nLarge Green Products:");
            var combinedSpecification = new AndSpecification<Product>(colorSpecification, sizeSpecification);
            foreach (Product product in filter.Filter(products, combinedSpecification))
            {
                Console.WriteLine($" - {product.Name} is large and green");
            }
        }
    }
    #endregion

}
