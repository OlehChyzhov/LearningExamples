namespace DesignPatterns.Builder.Functional_Builder
{
    /// <summary>
    /// Generic functional builder for creating objects of type TSubject.
    /// Stores build steps as functions and applies them when Build() is called.
    /// </summary>
    /// <typeparam name="TSubject">Type of object to build (must have parameterless constructor).</typeparam>
    /// <typeparam name="TSelf">Concrete builder type, enables fluent method chaining.</typeparam>
    public class GenericFunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
        where TSelf : GenericFunctionalBuilder<TSubject, TSelf>
    {
        private readonly List<Func<TSubject, TSubject>> actions = new();

        /// <summary>
        /// Adds a build step to modify the object.
        /// </summary>
        public TSelf Do(Action<TSubject> action)
        {
            actions.Add(obj =>
            {
                action(obj);
                return obj;
            });
            return (TSelf)this;
        }

        /// <summary>
        /// Creates a new TSubject instance and applies all build steps.
        /// </summary>
        public TSubject Build()
        {
            return actions.Aggregate(new TSubject(), (obj, func) => func(obj));
        }
    }

    /// <summary>
    /// Concrete builder for Person, inherits from GenericFunctionalBuilder.
    /// Adds semantic methods specific to Person.
    /// </summary>
    public class InheritedPersonBuilder : GenericFunctionalBuilder<Person, InheritedPersonBuilder>
    {
        public InheritedPersonBuilder Called(string name)
        {
            return Do(p => p.Name = name);
        }

        public InheritedPersonBuilder WorksAs(string position)
        {
            return Do(p => p.Position = position);
        }
    }
}