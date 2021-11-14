import axios from 'axios';
import { LogE } from './Utilities';

const developmentApi = "https://localhost:7201/api/";
const productionApi = "http://posthumanbackapp-001-site1.btempurl.com/api/";
const ApiUrl = (process.env.NODE_ENV === "development") ? developmentApi : productionApi;

const Api = {
    Get: (entityName, successCallback, errorCallback) => {
        axios
            .get(ApiUrl + entityName)
            .then(response => { successCallback(response.data); })
            .catch(error => {
                LogE("Error occured when calling GET method for " + entityName, error)
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Put: (entityName, entityId, entityData, successCallback, errorCallback) => {
        axios
            .put(ApiUrl + entityName + "/" + entityId, entityData)
            .then(response => { successCallback(response.data); })
            .catch(error => {
                LogE("Error occured when calling PUT method for " + entityName, error)
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Post: (entityName, entityData, successCallback, errorCallback) => {
        axios
            .post(ApiUrl + entityName, entityData)
            .then(response => { successCallback(response.data); })
            .catch(error => {
                LogE("Error occured when calling POST method for " + entityName, error)
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Delete: (entityName, entityId, successCallback, errorCallback) => {
        axios
            .delete(ApiUrl + entityName + "/" + entityId)
            .then(response => { successCallback(response.data); })
            .catch(error => {
                LogE("Error occured when calling DELETE method for " + entityName, error)
                if (errorCallback)
                    errorCallback(error);
            });
    }
}

export default Api;