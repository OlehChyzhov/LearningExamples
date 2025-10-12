namespace DesignPatterns.Factories.Bulk_Replacement
{
    // A simple mutable wrapper around a reference type
    // Allows external references to have their .Value reassigned
    public class Wrapper<T> where T : class
    {
        public T Value;

        public Wrapper(T value)
        {
            Value = value;
        }
    }

    // Factory that allows for bulk replacement of themes
    public class ReplaceableThemeFactory
    {
        // List of references to all created themes
        private readonly List<Wrapper<ITheme>> themes = new();

        // Utility method to create a theme based on the 'isDark' flag
        private ITheme CreateThemeImplementation(bool isDark)
        {
            return isDark ? new DarkTheme() : new LightTheme();
        }

        // Creates a new theme and wraps it in a Ref<T>
        // This indirection allows the theme to be replaced later
        public Wrapper<ITheme> CreateTheme(bool isDark)
        {
            Wrapper<ITheme> wrapper = new Wrapper<ITheme>(CreateThemeImplementation(isDark));
            themes.Add(wrapper); // Track the reference for later replacement
            return wrapper;
        }

        // Replaces the .Value of each theme Ref with a new theme
        public void ReplaceTheme(bool isDark)
        {
            foreach (Wrapper<ITheme> wrapper in themes)
            {
                wrapper.Value = CreateThemeImplementation(isDark);
            }
        }
    }


    #region Example
    // Example demonstrating how ReplaceableThemeFactory supports bulk replacement
    public static class BulkReplacement_Example
    {
        public static void Run()
        {
            ReplaceableThemeFactory factory = new ReplaceableThemeFactory();

            // Create two themes with different initial styles
            Wrapper<ITheme> theme1 = factory.CreateTheme(true);   // dark
            Wrapper<ITheme> theme2 = factory.CreateTheme(false);  // light

            Console.WriteLine("Theme1 background: " + theme1.Value.BackgroundColor);
            Console.WriteLine("Theme2 background: " + theme2.Value.BackgroundColor);

            // Replace all themes created so far with a new LightTheme
            factory.ReplaceTheme(false);

            // After replacement, both references now point to LightTheme
            Console.WriteLine("Replaced theme1 background: " + theme1.Value.BackgroundColor);
            Console.WriteLine("Replaced theme2 background: " + theme2.Value.BackgroundColor);
        }
    }
    #endregion
}
