import { LogI } from './Utilities';

// Helper functions that makes working with arrays and objects easier
// Also makes handling react state civilized  


// Returns true if array has at least one item
// Returns false when array does not exist or have no items 
const ArrayHasElements = (array) => {
    return (typeof array !== 'undefined' && array.length);
};

// Returns array (subset) of objects from given array matching given property value
const FindObjects = (array, property, value) => {
    let foundObjects = null;

    if (ArrayHasElements(array)) {
        foundObjects  = array.filter(obj => {
            return obj[property] === value;
        });
    }
    
    return foundObjects;
};

// Returns all children objects of given parent item
// Objects are identificated by 'parentId' property
// TODO: extend function so it can collect child objects by other properties
const GetAllChildren = (array, parentItem) => {
    let items = [];
    items.push(parentItem);

    let childItems = FindObjects(array, 'parentId', parentItem.id);
    
    // Has child items - iterating...
    if(ArrayHasElements(childItems)) {
        childItems.map(childItem => {
            let childrenArray = GetAllChildren(array, childItem);
            items.push(...childrenArray);
        });
    };

    return items;
};

// Returns index (position) of first object in the given array that match value of given property
// If no object matches given property value, -1 is returned
// Examples: 
//      var index = FindObjectIndexByProperty(todoItems, "id", 15); 
//      var index = FindObjectIndexByProperty(todoItems, "name", "Task title 1");
const FindObjectIndexByProperty = (array, property, value) => {
    if (ArrayHasElements(array)) {
        return array.map(function (obj) { return obj[property]; }).indexOf(value);
    }
    else {
        return -1;
    }
};


const FindObjectByProperty = (array, property, value) => {
    let foundObject = null;
    if (ArrayHasElements(array)) {
        let index = FindObjectIndexByProperty(array, property, value);
        if (index > 0)
            foundObject = array[index];
    }

    return foundObject;
};

// Returns copy of given array
// References to array elements are never equal  
const CopyArray = (array) => {
    if (ArrayHasElements(array)) {
        var arrayCopy = array.map(item => { return { ...item } });
        return arrayCopy;
    } else {
        return null;
    }
};

// Removes object from array by given property value 
// Examples
//      var arrayWithoutItem = RemoveObjectFromArray(array, "id", 5);
//      var arrayWithoutItem = RemoveObjectFromArray(array, "title", "Some title");
// Returns array copy
const RemoveObjectFromArray = (array, property, value) => {
    if (ArrayHasElements(array)) {
        const arrayCopy = array.filter((existingItem) => existingItem[property] !== value);
        return arrayCopy;
    } else {
        return null;
    }
};

// Inserts object to array at given index
// Returns array copy
const InsertObjectAtIndex = (sourceArray, objectToInsert, index) => {
    if(ArrayHasElements(sourceArray)) {
        var arrayCopy = CopyArray(sourceArray);
        arrayCopy.splice(index, 0, objectToInsert);
        return arrayCopy;
    }
    else {
        var newArray = [];
        newArray.push(objectToInsert);
        return newArray;    
    }
};

// Inserts object at the end of array
// Returns array copy
const InsertObject = (sourceArray, objectToInsert) => {
    var arrayCopy = CopyArray(sourceArray);
    arrayCopy = [...arrayCopy, objectToInsert];
    return arrayCopy;
};

// Returns exact copy of given object
const CopyObject = (objectToCopy) => {
    return { ...objectToCopy };
};

// Returns copy of object with modified property 
const UpdateObjectProperty = (sourceObject, property, value) => {
    return { ...sourceObject, [property]: value };
};

// Replaces first object in array that has the same 'property' value
// Examples:
//      var newArray = ReplaceObjectInArray(oldArray, objectToReplace, "id", 5);                -   replaces object with id=5
//      var newArray = ReplaceObjectInArray(oldArray, objectToReplace, "title", "Some title");  -   replaces object with title="Some title"
// Returns array copy
const ReplaceObjectInArray = (array, object, property, value) => {
    if (ArrayHasElements(array)) {
        return array.map((obj) => obj[property] === value ? object : obj)
    } else {
        return null;
    }
};

// Updates object property in array
// Returns array copy
const UpdateObjectInArray = (array, object, property, value) => {

};

export {
    ArrayHasElements,

    CopyObject,
    CopyArray,

    FindObjects,
    GetAllChildren,

    

    UpdateObjectProperty,
    UpdateObjectInArray,

    FindObjectIndexByProperty,
    FindObjectByProperty,

    ReplaceObjectInArray,

    InsertObject,
    InsertObjectAtIndex,
    RemoveObjectFromArray
}