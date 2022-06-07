import { isEmpty, isNull, isUndefined } from "lodash";

const FindItemById = <T>(array: T[], id: number): T => {
    ThrowIfNullOrEmpty(array);
    if (id < 0) ThrowParamError("id", "Id should be greater than 0");

    let itemsFound = FindItemsByProperty(array, "id", id);
    return itemsFound[0];
};

// Returns item at given index
const FindItemByIndex = <T>(array: T[], index: number): T => {
    ThrowIfNullOrEmpty(array);

    if (index < 0 || index > array.length)
        ThrowParamError(
            "index",
            "Index should be greater than 0 and smaller than array length."
        );

    return array[index];
};

// Returns array (subset) of objects from given array matching given property value
const FindItemsByProperty = <T>(array: T[], propertyName: string, propertyValue: any): T[] => {
    ThrowIfNullOrEmpty(array);

    let itemsFound = new Array<T>();
    itemsFound = array.filter(
        (item: T) => item[propertyName as keyof T] === propertyValue
    );

    return itemsFound;
};

// Returns true if array has at least one item
// Returns false when array does not exist or have no items
 const HasAnyItem = <T>(array: T[]): boolean => {
     return array.length > 0;
 }

 export default HasAnyItem;

//#region Exceptions
function ThrowParamError(parameterName: string, errorMessage: string) {
    throw new Error(
        `Parameter error - '${parameterName}'. Message: ${errorMessage}`
    );
}

function ThrowIfNull<T>(array: T[]) {
    if (isNull(array) || isUndefined(array))
        ThrowParamError("array", "Provided array is null or undefined");
}

function ThrowIfEmpty<T>(array: T[]) {
    if (isEmpty(array)) ThrowParamError("array", "Provided array is empty.");
}

function ThrowIfNullOrEmpty<T>(array: T[]) {
    ThrowIfNull(array);
    ThrowIfEmpty(array);
}
//#endregion Exceptions

/*

let emptyArray: string[] = []; 
let strings: string[] = ["qwe", "tre", "BVC", "EEE"];
let numbers: number[] = [1, 2, 5, 8, 9];
let bools: boolean[] = [true, false, false];
let objArray1 = [{ id: 1, prop1: "Jacek",   prop2: 24 },    { id: 2,    prop1: "Agatka", prop2: 32 },   { id: 3, prop1: "Sraadek", prop2: 666 }];
let objArray2 = [{ id: 4, prop1: "Kiara",   prop2: 14 },    { id: 6,    prop1: "Tekuwa", prop2: 352 },  { id: 11, prop1: "Janrodo", prop2: 2 }];
let objArray3 = [{ id: 1, prop1: "cxzcssxzc", prop2: 312 },   { id: 87,   prop1: "Sraadek", prop2: 66265 }];

ArrayUtils.FindItemById(null, 1);
ArrayUtils.FindItemById(undefined, 1);
ArrayUtils.FindItemById([], 3);
ArrayUtils.FindItemById(objArray3, -4);

let obj1 = ArrayUtils.FindItemById(objArray1, 1);
let obj2 = ArrayUtils.FindItemById(objArray2, 5);

let obj3 = ArrayUtils.FindItemByIndex(objArray3, 1);
let obj4 = ArrayUtils.FindItemByIndex(objArray2, 2);
let obj5 = ArrayUtils.FindItemByIndex(objArray2, -1);

Log(obj1);
Log(obj2);
Log(obj3);
Log(obj4);
Log(obj5);

function LogInfo(array, property, value) {
    Log(`Array: ${array}`);
    Log(`Property name: ${property}`);
    Log(`Property value: ${value}`);
}

function Log(text) {
    console.log(text);
}
 
*/

export { FindItemById, FindItemByIndex, FindItemsByProperty };