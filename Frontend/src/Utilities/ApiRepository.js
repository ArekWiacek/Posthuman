import axios from 'axios';
import { LogW, LogE } from './Utilities';

const developmentApi = "https://localhost:7201/api/";
const productionApi = "http://posthumanae-001-site1.itempurl.com/api/";

const ApiUrl = (process.env.NODE_ENV == "development") ? developmentApi : productionApi;

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