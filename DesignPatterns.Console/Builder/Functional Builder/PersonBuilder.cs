namespace DesignPatterns.Builder.Functional_Builder
{
    #region Default Objects
    /// <summary>
    /// The class representing the complex object we want to build.
    /// In this example, a simple Person with Name and Position.
    /// </summary>
    public class Person
    {
        // Person's full name
        public string Name { get; set; }

        // Person's job or role
        public string Position { get; set; }
    }
    #endregion

    /// <summary>
    /// This is a Functional Builder for Person.
    /// Instead of setting fields immediately, it stores a list of actions (functions) 
    /// that describe how to build the object.
    /// When Build() is called, all these actions are applied in order to create the final Person.
    /// 
    /// This approach allows for flexible, composable, and easy-to-extend object construction.
    /// </summary>
    public sealed class PersonBuilder
    {
        // This list stores all the building steps as functions.
        // Each function takes a Person, modifies it, and returns it.
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        /// <summary>
        /// Adds an action to set the person's name.
        /// Returns the builder to allow method chaining.
        /// </summary>
        public PersonBuilder Called(string name)
        {
            // Use the helper Do() method to add the action
            return Do(p => p.Name = name);
        }

        /// <summary>
        /// Adds a custom action to the builder.
        /// Allows flexibility to modify Person in any way.
        /// </summary>
        public PersonBuilder Do(Action<Person> action)
        {
            // Convert the void Action<Person> into a Func<Person, Person> and add to list
            return AddAction(action);
        }

        /// <summary>
        /// Executes all the accumulated actions on a new Person object,
        /// returning the fully built Person.
        /// </summary>
        public Person Build()
        {
            // Start with a fresh Person instance
            // Apply each function in sequence, passing along the modified Person
            return actions.Aggregate(new Person(), (person, func) => func(person));
        }

        /// <summary>
        /// Converts an Action<Person> into a Func<Person, Person> to support chaining.
        /// Adds the function to the list of build steps.
        /// </summary>
        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(person =>
            {
                action(person);  // Perform the action (e.g., set property)
                return person;   // Return the modified Person for further chaining
            });
            return this;
        }
    }

    /// <summary>
    /// Extension methods allow us to add more building steps without modifying PersonBuilder.
    /// This keeps the builder open for extension but closed for modification (Open/Closed Principle).
    /// </summary>
    public static class PersonBuilderExtensions
    {
        /// <summary>
        /// Adds a step to set the job position.
        /// </summary>
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
        {
            return builder.Do(p => p.Position = position);
        }
    }

    #region Example
    /// <summary>
    /// Demonstrates usage of the functional builder.
    /// Shows how to fluently build a Person with name and job.
    /// </summary>
    public static class FunctionalBuilder_Example
    {
        public static void Run()
        {
            // Build a Person named Sarah who works as a Developer
            Person person = new PersonBuilder()
                                .Called("Sarah")
                                .WorksAs("Developer")
                                .Build();

            // Output: Name: Sarah, Job: Developer
            Console.WriteLine($"Name: {person.Name}, Job: {person.Position}");
        }
    }
    #endregion
}