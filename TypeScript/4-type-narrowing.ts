// ------------------------------------Typeof Guards------------------------------------

/*
    A function that handles both strings and numbers using type narrowing.
    - The `typeof` operator is used to determine the runtime type.
    - If it's a number, it returns the number multiplied by 3.
    - If it's a string, it repeats the string 3 times.
*/
function triple(value: number | string): number | string {
  if (typeof value === "number") {
    return value * 3;
  }
  return value.repeat(3);
}

// ------------------------------------'in' Operator Narrowing------------------------------------

/*
    Interfaces representing two different types of media with different properties.
    - 'Movie' has 'duration'.
    - 'TVShow' has 'numEpisodes' and 'episodeDuration'.
*/
interface Movie {
  title: string;
  duration: number;
}

interface TVShow {
  title: string;
  numEpisodes: number;
  episodeDuration: number;
}

/*
    Function to compute runtime for a movie or TV show.
    - Uses the 'in' operator to check if 'numEpisodes' exists in the object.
    - This allows TypeScript to safely infer the type at runtime.
*/
function getRuntime(media: Movie | TVShow): number {
  if ("numEpisodes" in media) {
    return media.numEpisodes * media.episodeDuration;
  }
  return media.duration;
}

// ------------------------------------Type Predicates------------------------------------

/*
    Interfaces for different animals.
    - Cat has a unique property: 'numLives'.
    - Dog has a unique property: 'breed'.
*/
interface Cat {
  name: string;
  numLives: number;
}

interface Dog {
  name: string;
  breed: string;
}

/*
    Custom type predicate function.
    - The return type `animal is Cat` tells TypeScript how to narrow the type.
    - Used for advanced control over type checking in complex conditions.
*/
function isCat(animal: Cat | Dog): animal is Cat {
  return (animal as Cat).numLives !== undefined;
}

/*
    Uses the custom type guard 'isCat' to return different sounds.
    - If 'animal' is a Cat, return "Meow".
    - Otherwise, it's a Dog → return "Woof".
*/
function makeNoise(animal: Cat | Dog): string {
  if (isCat(animal)) {
    return "Meow";
  }
  return "Woof";
}

// ------------------------------------Discriminated Unions------------------------------------

/*
    Interfaces representing different farm animals.
    - Each has a common structure and a unique literal property 'kind'.
    - 'kind' is used as a discriminator to narrow the union type safely.
*/
interface Rooster {
  kind: "rooster";
  name: string;
  weight: number;
  age: number;
}

interface Cow {
  kind: "cow";
  name: string;
  weight: number;
  age: number;
}

interface Pig {
  kind: "pig";
  name: string;
  weight: number;
  age: number;
}

// A union of all farm animals with a discriminated property
type FarmAnimal = Pig | Rooster | Cow;

/*
    Function that returns an animal sound based on the 'kind' discriminator.
    - A 'switch' is used for type-safe branching.
    - Each case handles a specific animal.
*/
function getFarmAnimalSound(animal: FarmAnimal): string {
  switch (animal.kind) {
    case "pig":
      return "Oink!";
    case "cow":
      return "Moo!";
    case "rooster":
      return "Cockadoodledoo!";
  }
}

// ------------------------------------Exhaustiveness Checks With Never------------------------------------

/*
    Improved version of the previous function that ensures all cases are handled.
    - The 'default' case assigns the value to a variable of type 'never'.
    - If a new animal type is added but not handled, TypeScript will throw a type error here.
    - This pattern is useful for safe refactoring and scaling.
*/
function getFarmAnimalSound2(animal: FarmAnimal): string {
  switch (animal.kind) {
    case "pig":
      return "Oink!";
    case "cow":
      return "Moo!";
    case "rooster":
      return "Cockadoodledoo!";
    default:
      // If this line is reached, it means an unhandled type was passed
      const value: never = animal;
      // Optionally throw an error or return fallback
      return "Unknown animal!";
  }
}