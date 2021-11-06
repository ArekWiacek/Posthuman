import axios from 'axios';
import { LogT, LogI, LogW, LogE } from './Utilities';


class RequestType {
    static Get = 0;
    static Put = 1;
    static Post = 2;
    static Delete = 3;

    static get GET() { return this.Get; }
    static get PUT() { return this.Put; }
    static get POST() { return this.Post; }
    static get DELETE() { return this.Delete; }
}

const ApiUrl = "https://localhost:7201/api/";

const ApiGet = (entityName, successCallback, errorCallback) => {
    axios
        .get(ApiUrl + entityName)
        .then(response => {
            successCallback(response.data);
        })
        .catch(error => { 
            LogE("Error occured when calling GET method for " + entityName, error)
            if(errorCallback)
                errorCallback(error);
        });
}

const ApiPut = (entityName, entityId, entityData, successCallback, errorCallback) => {
    axios
        .put(ApiUrl + entityName + "/" + entityId, entityData)
        .then(response => {
            successCallback(response.data);
        })
        .catch(error => { 
            LogE("Error occured when calling PUT method for " + entityName, error)
            if(errorCallback)
                errorCallback(error);
        });
}

const ApiPost = (entityName, entityData, successCallback, errorCallback) => {
    axios
        .post(ApiUrl + entityName, entityData)
        .then(response => {
            successCallback(response.data);
        })
        .catch(error => { 
            LogE("Error occured when calling POST method for " + entityName, error)
            if(errorCallback)
                errorCallback(error);
        });
}

const ApiDelete = (entityName, entityId, successCallback, errorCallback) => {
    axios
        .delete(ApiUrl + entityName + "/" + entityId)
        .then(response => {
            successCallback(response.data);
        })
        .catch(error => { 
            LogE("Error occured when calling DELETE method for " + entityName, error)
            if(errorCallback)
                errorCallback(error);
        });
}

export { ApiGet, ApiPost, ApiPut, ApiDelete }