// ------------------------------------Class Example------------------------------------
/*
    Notes:
    1. TypeScript requires class properties to be declared before they are used in the constructor.
    2. TS introduces access modifiers: 
       - 'public' (default) – accessible everywhere,
       - 'private' – accessible only within the class,
       - 'protected' – accessible in the class and its subclasses.
    3. 'private' in TS is not the same as private fields in JS (e.g., #field); it's enforced only by the TypeScript compiler.
*/

class Player {
  readonly firstName: string; // Cannot be reassigned after construction
  readonly lastName: string;
  protected _score: number = 0; // Accessible within the class and subclasses

  constructor(firstName: string, lastName: string) {
    this.firstName = firstName;
    this.lastName = lastName;
  }

  // Getter method for '_score'
  get score(): number {
    return this._score;
  }

  // Setter method with validation logic
  set score(newScore: number) {
    if (newScore < 0) {
      throw new Error("Score can't be < 0");
    }
    this._score = newScore;
  }
}

// Example of inheritance and using 'protected' field
class PlayerAdmin extends Player {
  maxScore() {
    this._score = 1000; // Allowed: '_score' is protected
  }
}

// Shortcut syntax: TypeScript allows auto-defining properties via constructor parameters
class PlayerShortcut {
  constructor(public firstName: string, public lastName: string) {} // Auto-creates and initializes fields
}

// ------------------------------------Class with Interfaces------------------------------------

/*
    Interfaces define the shape of objects or classes — a contract to follow.
    A class can implement multiple interfaces using a comma-separated list.
*/

interface IColorful {
  color: string; // Any object or class implementing this must have a 'color' property
}

interface IPrintable {
  print(): void; // Must implement a 'print' method
}

// A class implementing multiple interfaces
class Jacket implements IColorful, IPrintable {
  constructor(public color: string) {}

  print(): void {
    console.log(`This jacket is ${this.color}.`);
  }
}

// ------------------------------------Abstract Classes------------------------------------

/*
    Abstract classes:
    - Cannot be instantiated directly.
    - Can contain both implemented and abstract (unimplemented) methods.
    - Use when you want to provide shared behavior + require subclasses to implement specific methods.
*/

abstract class BaseEmployee {
  constructor(public firstName: string, public lastName: string) {}

  // Abstract method: must be implemented by derived classes
  abstract getPay(): number;

  // Concrete method: shared implementation
  sayHi() {
    console.log(`"${this.firstName} ${this.lastName}" says "Hi!"`);
  }
}

// Full-time employee class extends abstract class and implements required method
class FullTimeEmployee extends BaseEmployee {
  constructor(
    public firstName: string,
    public lastName: string,
    private readonly salary: number // private: only accessible within this class
  ) {
    super(firstName, lastName); // Calls BaseEmployee constructor
  }

  getPay(): number {
    return this.salary; // Implementation of abstract method
  }
}

// ------------------------------------Usage Example------------------------------------

// Creating an instance of a subclass
const employee = new FullTimeEmployee("Walter", "White", 999_999);

// Calling inherited method from abstract class
employee.sayHi(); // Output: "Walter White says "Hi!""
