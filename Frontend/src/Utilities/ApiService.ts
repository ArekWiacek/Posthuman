import axios from 'axios';
import { LogE } from './Utilities';

const developmentApi: string = "https://localhost:7201/api/";
const productionApi: string = "http://posthumanbackapp-001-site1.btempurl.com/api/";
const ApiUrl: string = (process.env.NODE_ENV === "development") ? developmentApi : productionApi;

/*
const Api = {
    Get: (entityName, successCallback, errorCallback) => {
        axios
            .get(ApiUrl + entityName, { withCerdentials: true })
            .then(response => successCallback(response.data))
            .catch(error => {
                LogE("Error occured when calling GET method for " + entityName, error);
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Put: (entityName, entityId, entityData, successCallback, errorCallback) => {
        axios
            .put(ApiUrl + entityName + "/" + entityId, entityData)
            .then(response => successCallback(response.data))
            .catch(error => {
                LogE("Error occured when calling PUT method for " + entityName, error);
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Post: (entityName, entityData, successCallback, errorCallback) => {
        axios
            .post(ApiUrl + entityName, entityData, { withCredentials: true })
            .then(response => successCallback(response.data))
            .catch(error => {
                //var errorJson = error.toJSON();
                LogE("Error occured when calling POST method for " + entityName, error);
                if (errorCallback)
                    errorCallback(error);
            });
    },

    Delete: (entityName, entityId, successCallback, errorCallback) => {
        axios
            .delete(ApiUrl + entityName + "/" + entityId)
            .then(response => successCallback(response.data))
            .catch(error => {
                LogE("Error occured when calling DELETE method for " + entityName, error);
                if (errorCallback)
                    errorCallback(error);
            });
    }
}

export default Api;*/