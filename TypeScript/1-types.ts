// ------------------------------------Type Annotations------------------------------------
// Example of defining types
const patient: string = "Jessy"; // 'patient' must be a string
let cleanDays: number; // Number of clean (drug-free) days, must be a number

// TypeScript can infer types automatically (Type Inference)
let patientAddress = "9809 Margo Street, Albuquerque, New Mexico"; // inferred as string

// The 'any' type disables type checking — use only if necessary and with caution
let unknownThing: any;

// Type assertion: tells TypeScript the actual type of a variable
let secretIdentity: unknown = "Heisenberg";
const aliasLength = (secretIdentity as string).length; // Assert 'secretIdentity' as string to get length

// ------------------------------------Functions------------------------------------

// Function Parameter Annotations: 'purity' must be a number
function cookMeth(purity: number) {
  return purity * purity; // Example operation on number
}

// Functions with Default Parameters:
// If no argument provided, defaults are used; types are inferred automatically
function introduceCharacter(character = "Walter") {} // 'character' inferred as string
function greetPhrase(phrase: string = "I am the one who knocks") {} // explicit string type

// Function Return Type:
// Function must return a string
function getNickname(): string {
  return "Heisenberg";
}

// Anonymous Functions:
// TypeScript infers parameter types and return types in callbacks
const cartelColors = ["blue", "white", "pink"];
cartelColors.map((color) => 3); // returns number array, ignores color param
cartelColors.map((color: string): number => 2); // explicitly typed color and return type

// The 'never' type is used for functions that never return (throw error or infinite loop)
function surrender(): never {
  throw Error("Say my name!"); // function throws error, never returns normally
}

function cookForever(): never {
  while (true) {} // infinite loop, never returns
}

// ------------------------------------Union Types & Arrays------------------------------------
// Union types allow a variable to hold values of multiple types
let moneyOrName: number | string;

// Literal Types restrict a variable to specific values only
let mood: "Heisenberg" | "Pinkman"; // 'mood' can only be one of these two strings
let zero: 0 = 0; // 'zero' can only be the number 0

// Array type definitions
const villains: string[] = ["Gus Fring", "Heisenberg"]; // array of strings
const villainAndIncome: (string | number)[] = [
  "Heisenberg",
  2_000_000,
  "Gus Fring",
  10_000_000,
]; // array containing strings and numbers

// Multidimensional Arrays: arrays of arrays of strings
const labGrid: string[][] = [
  ["Chemistry", "Extraction", "Cooking"],
  ["Packing", "Storage", "Security"],
  ["Escape", "Surveillance", "Armed"],
];

// Tuples: fixed-length arrays with known types in each position
let vehicle: [string, number] = ["RV", 10_000]; // tuple with a string and a number

// ------------------------------------Objects------------------------------------
// Object type declaration with explicit property types
let meetPlace: {
  street: string; // property 'street' is a string
  money: number;  // property 'money' is a number
};

meetPlace = {
  street: "123 Criminal Street",
  money: 1_000,
};

// Type aliases define custom object types for reuse
type SafeHouse = {
  street: string;
  money: number;
};

let meetPlace2: SafeHouse = {
  street: "456 Another Criminal Street",
  money: 2_500,
};

// Optional properties can be omitted when creating objects
type DesertLabCoordinates = {
  x: number;
  y: number;
  z?: number; // optional property 'z'
};

// Intersection types combine multiple types into one
type Chemist = {
  skillLevel: number;
};

type Boss = {
  reputation: string;
};

type ChemistBoss = Chemist & Boss & {
  age: number; // intersection adds extra property
};

// Object types in function parameters
function printPerson(person: { firstName: string; lastName: string }): void {}

// Function returning an object
function getCleaner(): { firstName: string } {
  return { firstName: "Mike" };
}

// Weird behavior explanation:
/*
You can pass an object variable with extra properties to a function expecting fewer,
and it will work fine. But passing an object literal with extra properties directly
in the function call causes an error.

Example:

// Works fine:
const person = { firstName: "Jesse", lastName: "Pinkman", extraField: 2 };
printPerson(person);

// Causes error:
printPerson({ firstName: "Jesse", lastName: "Pinkman", extraField: 2 });
*/

// ------------------------------------Enums------------------------------------
// Using 'const enum' means no object is created in JavaScript output, cleaner code
const enum RVDirections {
  UP = "UP",
  DOWN = "DOWN",
  LEFT = "LEFT",
  RIGHT = "RIGHT",
}

// ------------------------------------Interfaces------------------------------------
interface CrewMember {
  readonly id: number; // read-only property: can't be changed after assignment
  name: string;
  age: number;
  nickname?: string; // optional property

  introduce(): void; // method signature
}

// Interface merging: interfaces with the same name are combined
interface LabEquipment {
  isClean: boolean;
}

interface LabEquipment {
  color: string;
}

// Interface extension: new interface inherits properties from another
interface Chemical extends LabEquipment {}