// ============================= MODULE: mathUtils.ts =============================
// This section simulates a separate file for exports

// ------------------------------------EXPORTING FUNCTIONS------------------------------------

// A function that adds two numbers
export function add(x: number, y: number): number {
  return x + y;
}

// A generic function that returns the same value it receives
export function identity23<T>(item: T): T {
  return item;
}

// ------------------------------------EXPORTING TYPES------------------------------------

// A custom type alias for demonstration
export type Person = {
  name: string;
  age: number;
};

// ------------------------------------EXPORTING CLASSES------------------------------------

// A class with a static method
export class Calculator {
  static multiply(a: number, b: number): number {
    return a * b;
  }
}

// ------------------------------------DEFAULT EXPORT------------------------------------

// A default-exported function
export default function greet(): void {
  console.log("Hello from the default export!");
}


// ============================= MODULE: main.ts =============================
// This section simulates a second file that imports and uses the above exports

// ------------------------------------IMPORTING NAMED EXPORTS------------------------------------

// Import specific functions from the module
import { add, identity23 } from "./all-in-one";

console.log("Add:", add(2, 5)); // 7
console.log("Identity:", identity23("Walter")); // "Walter"

// ------------------------------------IMPORTING TYPES------------------------------------

// Importing types using `import type` (If not used, then empty js file will be created)
import type { Person } from "./all-in-one";
//import { type Person, <OtherClass> } from "./all-in-one";

const user: Person = {
  name: "Jesse Pinkman",
  age: 26
};
console.log("User:", user);

// ------------------------------------IMPORTING CLASSES------------------------------------

import { Calculator } from "./all-in-one";

console.log("Multiply:", Calculator.multiply(3, 7)); // 21

// ------------------------------------IMPORTING DEFAULT EXPORT------------------------------------

import greet from "./all-in-one";

greet(); // "Hello from the default export!"