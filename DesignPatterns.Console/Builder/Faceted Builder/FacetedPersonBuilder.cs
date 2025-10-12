namespace DesignPatterns.Builder.Faceted_Builder
{
    #region Default Objects
    // This is the main class representing the complex object we want to build.
    // It has multiple aspects (address and job) that can be set independently.
    public class Person
    {
        // Address-related fields
        public string StreetAddress, PostCode, City;

        // Job-related fields
        public string CompanyName, Position;
        public int AnnualIncome;

        // Provide a readable string representation of the Person object
        public override string ToString()
        {
            return $"StreetAddress: {StreetAddress}, PostCode: {PostCode}, City: {City}, " +
                   $"CompanyName: {CompanyName}, Position: {Position}, AnnualIncome: {AnnualIncome}";
        }
    }
    #endregion

    // Facade Builder: This class simplifies the process of building a Person object.
    // Instead of building all properties in one class, it delegates to specialized sub-builders.
    // This pattern is called the Faceted Builder.
    public class FacetedPersonBuilder
    {
        // The Person instance we are building. Shared between all sub-builders.
        protected Person person = new Person();

        // Sub-builder for job-related properties.
        // Every time you access Works, a new PersonJobBuilder is created with the same Person instance.
        public PersonJobBuilder Works => new PersonJobBuilder(person);

        // Sub-builder for address-related properties.
        // Similarly, a new PersonAddressBuilder is created on access, sharing the Person instance.
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        // Once building is done, this method returns the fully constructed Person object.
        public Person Build()
        {
            return person;
        }
    }

    // Sub-builder for setting address properties.
    // It receives the Person object reference and modifies only the address-related fields.
    public class PersonAddressBuilder : FacetedPersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            // Use the existing Person instance from the facade builder.
            this.person = person;
        }

        // Set the street address and return this builder for fluent chaining.
        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        // Set the postcode and return this builder for chaining.
        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.PostCode = postcode;
            return this;
        }

        // Set the city and return this builder for chaining.
        public PersonAddressBuilder InCity(string city)
        {
            person.City = city;
            return this;
        }
    }

    // Sub-builder for setting job properties.
    // It also shares the same Person instance and modifies only employment-related fields.
    public class PersonJobBuilder : FacetedPersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        // Set the company name and return this builder for chaining.
        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        // Set the job position and return this builder.
        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        // Set the annual income and return this builder.
        public PersonJobBuilder Earning(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    #region Example
    // Example usage of the Facade Builder pattern.
    // Shows how to build a Person with separate concerns (job and address) cleanly.
    public static class FacetedBuilder_Example
    {
        public static void Run()
        {
            FacetedPersonBuilder builder = new FacetedPersonBuilder();

            // Build the Person by specifying job info via Works sub-builder,
            // then specify address info via Lives sub-builder,
            // finally call Build() to get the fully constructed Person.
            Person person = builder
                .Works.At("Fabrikam")
                      .AsA("Engineer")
                      .Earning(123000)

                .Lives.At("123 London Road")
                      .InCity("London")
                      .WithPostcode("SW12AC")

                .Build();

            // Output the created Person object to console.
            Console.WriteLine(person);
        }
    }
    #endregion
}
