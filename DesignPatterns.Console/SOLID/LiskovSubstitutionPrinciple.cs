namespace DesignPatterns.SOLID
{
    // Liskov Substitution Principle (LSP) states:
    // "Objects of a superclass should be replaceable with objects of a subclass without breaking the application."
    // In other words, an instance of the base class should be replaceable with an instance of a subclass.

    #region Problem

    /// <summary>
    /// This is our base class Employee with two virtual methods: one for salary, one for bonus.
    /// </summary>
    public class Employee
    {
        protected string _name;
        protected int _salary;

        public Employee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public virtual int CalculateSalary() => _salary;

        public virtual int GetBonus() => 1000;
    }

    /// <summary>
    /// ContractualEmployee is a subclass of Employee.
    /// It overrides the salary method, but doesn't support bonuses at all.
    /// However, it is still forced to implement GetBonus, which it breaks by throwing an exception.
    /// </summary>
    public class ContractualEmployee : Employee
    {
        public ContractualEmployee(string name, int salary) : base(name, salary) { }

        public override int CalculateSalary()
        {
            return (int)(_salary - _salary * 0.33); // 33% tax deduction
        }

        public override int GetBonus()
        {
            throw new NotImplementedException("Contractual employees do not receive bonuses.");
        }
    }

    /// <summary>
    /// This method demonstrates the LSP violation.
    /// Replacing an Employee instance with a ContractualEmployee breaks the behavior.
    /// </summary>
    public static class LSP_ViolationExample
    {
        public static void Run()
        {
            List<Employee> employees = new()
            {
                new Employee("John", 5000),
                new ContractualEmployee("Alex", 5000)
            };

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.GetType().Name} bonus: {emp.GetBonus()}");
            }
        }
    }

    // Output:
    // Employee bonus: 1000
    // Unhandled exception: System.NotImplementedException
    
    #endregion

    #region Solution

    /// <summary>
    /// Separate bonus-related behavior into its own interface.
    /// Only those employees that are eligible for bonuses implement it.
    /// This prevents unrelated classes from being forced to implement something they shouldn't.
    /// </summary>
    public interface IBonusEligible
    {
        int GetBonus();
    }

    public class ImprovedEmployee
    {
        protected string _name;
        protected int _salary;

        public ImprovedEmployee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public virtual int CalculateSalary() => _salary;
    }

    public class ImprovedRegularEmployee : ImprovedEmployee, IBonusEligible
    {
        public ImprovedRegularEmployee(string name, int salary) : base(name, salary) { }

        public int GetBonus() => 1000;
    }

    public class ImprovedContractualEmployee : ImprovedEmployee
    {
        public ImprovedContractualEmployee(string name, int salary) : base(name, salary) { }

        public override int CalculateSalary() => (int)(_salary - _salary * 0.33);
    }
    #endregion

    #region LSP_Example
    /// <summary>
    /// This method demonstrates the fixed design — LSP is preserved because
    /// we only call GetBonus on objects that implement IBonusEligible.
    /// </summary>
    public static class LSP_SolutionExample
    {
        public static void Run()
        {
            List<ImprovedEmployee> employees = new()
            {
                new ImprovedRegularEmployee("John", 5000),
                new ImprovedContractualEmployee("Alex", 5000)
            };

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.GetType().Name} salary: {emp.CalculateSalary()}");

                if (emp is IBonusEligible bonusEligible)
                {
                    Console.WriteLine($"Bonus: {bonusEligible.GetBonus()}");
                }
                else
                {
                    Console.WriteLine("Bonus: Not eligible");
                }

                Console.WriteLine();
            }
        }
    }

    // Output:
    // ImprovedRegularEmployee salary: 5000
    // Bonus: 1000
    //
    // ImprovedContractualEmployee salary: 3350
    // Bonus: Not eligible
    #endregion
}
