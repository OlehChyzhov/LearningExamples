// ------------------------------------Generic Examples------------------------------------

/*
    A simple generic function that returns the same value it receives.
    - Type parameter <T> allows it to work with any data type.
    - Type is inferred based on the argument.
    - This is called an identity function.
*/
function identity22<T>(item: T): T {
    return item;
}

/*
    A generic function that merges two different objects into one.
    - Takes two parameters of types T and U.
    - Returns a new object that combines all properties from both.
    - The resulting type is an intersection: T & U.
*/
function merge<T, U>(obj1: T, obj2: U): T & U {
    return {
        ...obj1,
        ...obj2
    };
}

// ------------------------------------Generic Constraints------------------------------------

/*
    Same as 'merge' but restricted to objects only.
    - The 'extends object' constraint prevents passing primitive types like strings or numbers.
    - This ensures safe object merging.
*/
function mergeObjects<T extends object, U extends object>(obj1: T, obj2: U): T & U {
    return {
        ...obj1,
        ...obj2
    };
}

/*
    A function that only accepts values that have a 'length' property.
    - The interface 'Lengthy' defines this contract.
    - Works with strings, arrays, or any custom object that includes a 'length' field.
    - Returns double the length of the input.
*/
interface Lengthy {
    length: number;
}

function printDoubleLength<T extends Lengthy>(thing: T): number {
    return thing.length * 2;
}

// ------------------------------------Default Type Parameters------------------------------------

/*
    A generic function that creates and returns an empty array of a given type.
    - The type T defaults to 'number' if not specified.
    - This makes the function flexible and safe without requiring an explicit type.
    
    Usage examples:
    - makeEmptyArray<string>() → string[]
    - makeEmptyArray() → number[] (default)
*/
function makeEmptyArray<T = number>(): T[] {
    return [];
}

// ------------------------------------Generic Classes------------------------------------

/*
    Interfaces representing different media types for demo purposes.
    - Both have a 'title' field, but differ in other properties.
*/
interface Song {
    title: string;
    artist: string;
}

interface Video {
    title: string;
    creator: string;
    resolution: string;
}

/*
    A generic class that acts as a queue (playlist) for any type.
    - Stores elements of type T in an array.
    - The 'add' method allows adding new elements of that type.
*/
class Playlist<T> {
    public queue: T[] = [];

    add(el: T) {
        this.queue.push(el);
    }
}

// Creating specific instances of the generic Playlist class
const songs = new Playlist<Song>();     // A playlist for Song objects
const videos = new Playlist<Video>();   // A playlist for Video objects